using System.Net;
using System.Net.Sockets;

namespace EasyTcpSocket
{
    public class TcpSocketClient
    {
        /// <summary>
        /// 接收到消息后触发的事件
        /// </summary>
        public event Event_ReceivedMessage OnReceivedMessage;

        /// <summary>
        /// 接收到的数据包
        /// </summary>
        private SocketTcpPack Pack = new SocketTcpPack();

        private Socket ClientSocket;

        public TcpSocketClient(Event_ReceivedMessage _onReceivedMessage)
        {
            OnReceivedMessage = _onReceivedMessage;
        }

        public bool Connect(string serverIp, int serverPort, out string errorMessage)
        {
            bool result = false;
            errorMessage = "";

            if (!IPAddress.TryParse(serverIp, out IPAddress ipAddress))
            {
                throw new ArgumentException("无效的ip");
            };

            if (serverPort < 0 || serverPort > 65535)
            {
                throw new ArgumentException("无效的port");
            }

            Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPEndPoint endPoint = new IPEndPoint(ipAddress, serverPort);
                tempSocket.Connect(endPoint);

                if (tempSocket.Connected)
                {
                    result = true;
                    ClientSocket = tempSocket;
                    ReceiveHelp receive = new ReceiveHelp(Pack, OnReceivedMessage);
                    ClientSocket.BeginReceive(Pack.Buffer, Pack.Offset, Pack.Size, SocketFlags.None, receive.AsyncReceive, ClientSocket);
                }
            }
            catch (Exception ex)
            {
                result = false;
                errorMessage = ex.Message;
            }
            return result;
        }

        public int Send(DataType type, byte[] data)
        {
            SendHelp sendHelp = new SendHelp();
            return sendHelp.Send(ClientSocket, type, data);
        }
    }
}