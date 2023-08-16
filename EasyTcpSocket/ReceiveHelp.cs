using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EasyTcpSocket
{
    /// <summary>
    /// 接受消息
    /// </summary>
    public class ReceiveHelp
    {   
        /// <summary>
        /// 接收到消息后触发的事件
        /// </summary>
        public event Event_ReceivedMessage OnReceivedMessage;

        private SocketTcpPack Pack = new SocketTcpPack();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="_socketTcpPack">接收到的数据包</param>
        /// <param name="_onOnReceivedMessage">接收到消息后的回调事件</param>
        public ReceiveHelp(SocketTcpPack _socketTcpPack, Event_ReceivedMessage _onOnReceivedMessage)
        {
            Pack = _socketTcpPack;
            OnReceivedMessage += _onOnReceivedMessage;
        }

        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="result"></param>
        public void AsyncReceive(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            try
            {
                string clientIP = socket.RemoteEndPoint.ToString();
                int dataSize = socket.EndReceive(result);
                if (dataSize > 0)
                {
                    Pack.UntiePack(dataSize);
                    if (Pack.IsComplete)
                    {
                        foreach (var item in Pack.DataList)
                        {
                            //OnReceivedMessage.BeginInvoke(clientIP, item.Content, item.DataLength, CallBack_ReceivedMessage, null);
                            OnReceivedMessage(clientIP, item.Content, item.DataLength);
                        }
                        Pack.Clear();
                    }
                }

                //接收下一条消息
                socket.BeginReceive(Pack.Buffer, Pack.Offset, Pack.Size, SocketFlags.None, AsyncReceive, socket);
            }
            catch (SocketException)
            {
                //string ip = socket.RemoteEndPoint.ToString();
                //Close(ip);
            }
        }

        private void CallBack_ReceivedMessage(IAsyncResult ar)
        {
            OnReceivedMessage.EndInvoke(ar);
        }
    }
}
