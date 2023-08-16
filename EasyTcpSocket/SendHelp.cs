using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EasyTcpSocket.Common;

namespace EasyTcpSocket
{
    public enum DataType
    {
        Message, File
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendHelp
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="clientSocket">发送方Socket</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="data">数据</param>
        /// <returns>发送完成的数据长度</returns>
        public int Send(Socket clientSocket, DataType dataType, byte[] data)
        {
            //数据包内容
            byte[] content = data;
            //数据包头部
            byte[] header = DataConverter.Int32ToByteArray(content.Length);
            //最终封装好的数据包，数据包首位 0 消息 1 文件，2-5位 数据长度
            byte[] dataToBeSend = new byte[content.Length + 5];
            dataToBeSend[0] = (byte)dataType;
            Array.Copy(header, 0, dataToBeSend, 1, header.Length);
            Array.Copy(content, 0, dataToBeSend, 5, content.Length);

            return clientSocket.Send(dataToBeSend);
        }
    }
}
