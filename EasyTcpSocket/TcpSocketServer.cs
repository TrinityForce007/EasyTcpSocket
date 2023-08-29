using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace EasyTcpSocket
{
    public class TcpSocketServer
    {
        private SessionContainer SessionContainer;

        event AsyncEventHandler NewClientConnected;

        event AsyncEventHandler<CloseEventArgs> ConnectionClosed;





        /// <summary>
        /// 有新客户端连接时触发的事件
        /// </summary>
        public event Event_NewClientConncected OnNewClientConncected;

        /// <summary>
        /// 接收到消息时触发的事件
        /// </summary>
        public event Event_ReceivedMessage OnReceivedMessage;

        /// <summary>
        /// 客户端Socket列表
        /// </summary>
        private ConcurrentDictionary<string, Socket> _clientSocketList = new ConcurrentDictionary<string, Socket>();

        /// <summary>
        /// 消息接收Buffer
        /// </summary>
        private ConcurrentDictionary<string, SocketTcpPack> _receiveBufferDic = new ConcurrentDictionary<string, SocketTcpPack>();

        private readonly IPAddress ServerIP;
        private readonly int ServerPort;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">要开启监听的ip</param>
        /// <param name="port">要开启监听的port</param>
        /// <param name="_onNewClientConncected">有新客户端连接时触发的事件</param>
        /// <param name="_onOnReceivedMessage">接收到消息时触发的事件</param>
        public TcpSocketServer(string ip, int port, Event_NewClientConncected _onNewClientConncected, Event_ReceivedMessage _onOnReceivedMessage)
        {
            if (!IPAddress.TryParse(ip, out IPAddress ipAddress))
            {
                throw new ArgumentException("无效的ip");
            };

            if (port < 0 || port > 65535)
            {
                throw new ArgumentException("无效的port");
            }

            SessionContainer = new SessionContainer();
            ServerIP = ipAddress;
            ServerPort = port;
            OnNewClientConncected += _onNewClientConncected;
            OnReceivedMessage += _onOnReceivedMessage;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(ServerIP, ServerPort);
            socket.Bind(endPoint);
            socket.Listen(100);
            socket.BeginAccept(AcceptConnect, socket);
        }

        /// <summary>
        /// 开始接收消息
        /// </summary>
        /// <param name="result"></param>
        private void AcceptConnect(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            Socket clientSocket = socket.EndAccept(result);
            string clientIP = clientSocket.RemoteEndPoint.ToString();

            _clientSocketList.TryAdd(clientIP, clientSocket);

            Task.Run(() =>
            {
                OnNewClientConncected(_clientSocketList.Keys.ToList<string>(), clientIP);
            });

            SocketTcpPack tcpPack = new SocketTcpPack(1024);
            _receiveBufferDic.TryAdd(clientIP, tcpPack);

            //开始接受客户端消息
            ReceiveHelp receive = new ReceiveHelp(_receiveBufferDic[clientIP], OnReceivedMessage);
            clientSocket.BeginReceive(_receiveBufferDic[clientIP].Buffer, _receiveBufferDic[clientIP].Offset, _receiveBufferDic[clientIP].Size, SocketFlags.None, receive.AsyncReceive, clientSocket);

            //接受下一个连接
            socket.BeginAccept(AcceptConnect, socket);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="remoteEndPointStr">客户端的IP和端口 etc：127.0.0.1:55056</param>
        /// <param name="type">数据类型</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public int Send(string remoteEndPointStr, DataType type, byte[] data)
        {
            SendHelp sendHelp = new SendHelp();
            return sendHelp.Send(_clientSocketList[remoteEndPointStr], type, data);
        }
    }
}