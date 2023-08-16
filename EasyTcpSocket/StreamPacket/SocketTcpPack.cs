using EasyTcpSocket.Common;

namespace EasyTcpSocket
{
    /// <summary>
    /// 接受到的数据存储到这里，并做粘包和分包判断
    /// </summary>
    public class SocketTcpPack
    {
        /// <summary>
        /// 接收是否完成了
        /// </summary>
        public bool IsComplete = false;

        /// <summary>
        /// 接收缓存
        /// </summary>
        public byte[] Buffer;

        /// <summary>
        /// 下次接收从Buffer的哪里开始写入
        /// </summary>
        public int Offset = 0;

        /// <summary>
        /// 下次写入Buffer的长度
        /// </summary>
        public int Size;

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public List<ReceiveDataModel> DataList = new List<ReceiveDataModel>();

        /// <summary>
        /// 缓存长度
        /// </summary>
        private readonly int BufferLength;

        public SocketTcpPack(int bufferLength = 1024)
        {
            BufferLength = bufferLength;
            Buffer = new byte[BufferLength];
            Size = BufferLength;
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="currentDataSize">接收到的数据长度，Socket.Receive()方法返回的数值</param>
        public void UntiePack(int currentDataSize)
        {
            //Size != BufferLength说明Buffer中保留了一些上次接收的数据，要把这部分数据长度加上
            int dataSize = currentDataSize;
            if (Size != BufferLength)
            {
                dataSize += Offset;
            }

            if (DataList.Count == 0)
            {
                SplitData(Buffer, dataSize);
            }
            else
            {
                //取出DataList中的最后一个元素，通过判断这个元素是否完整来判断是有分包需要补充完整
                ReceiveDataModel LastReceiveData = DataList[DataList.Count - 1];
                if (LastReceiveData.IsComplete)
                {
                    SplitData(Buffer, dataSize);
                }
                else
                {
                    //最后一个包的剩余长度
                    int remainingDataLength = LastReceiveData.DataLength - LastReceiveData.Content.Length;
                    //剩余长度 < 本次接收的数据长度，说明这一次接收就可以把上一个分包补充完整
                    if (remainingDataLength < dataSize)
                    {
                        int realLength = LastReceiveData.Content.Length;
                        byte[] b = new byte[LastReceiveData.DataLength];
                        Array.Copy(LastReceiveData.Content, 0, b, 0, LastReceiveData.Content.Length);
                        LastReceiveData.Content = b;
                        Array.Copy(Buffer, 0, LastReceiveData.Content, realLength, remainingDataLength);

                        //继续处理剩下的数据
                        byte[] last = new byte[dataSize - remainingDataLength];
                        Array.Copy(Buffer, remainingDataLength, last, 0, last.Length);
                        SplitData(last, last.Length);
                    }
                    //剩余长度 > 本次接收的数据长度，说明这一次接收还不能把上一个分包补充完整，还需要继续等待接收
                    else if (remainingDataLength > dataSize)
                    {
                        int realLength = LastReceiveData.Content.Length;
                        byte[] b = new byte[LastReceiveData.Content.Length + dataSize];
                        Array.Copy(LastReceiveData.Content, 0, b, 0, LastReceiveData.Content.Length);
                        LastReceiveData.Content = b;
                        Array.Copy(Buffer, 0, LastReceiveData.Content, realLength, dataSize);

                        Offset = 0;
                        Size = BufferLength;
                        Buffer = new byte[BufferLength];
                    }
                    else
                    {
                        int realLength = LastReceiveData.Content.Length;
                        byte[] b = new byte[LastReceiveData.DataLength];
                        Array.Copy(LastReceiveData.Content, 0, b, 0, LastReceiveData.Content.Length);
                        LastReceiveData.Content = b;
                        Array.Copy(Buffer, 0, LastReceiveData.Content, realLength, remainingDataLength);

                        Offset = 0;
                        Size = BufferLength;
                        Buffer = new byte[BufferLength];
                        IsComplete = true;
                    }
                }
            }
        }

        /// <summary>
        /// 处理byte[]前5位就是包首部的这种数据
        /// </summary>
        /// <param name="data">byte[]</param>
        /// <param name="dataSize">内容的实际长度</param>
        private void SplitData(byte[] data, int dataSize)
        {
            //长度 <= 5 说明包首部还没有接收完成，需要继续接收
            if (dataSize <= 5)
            {
                byte[] temp = new byte[BufferLength];
                Array.Copy(data, 0, temp, 0, dataSize);
                Buffer = temp;
                Offset = dataSize;
                Size = BufferLength - dataSize;
                IsComplete = true;
                return;
            }

            //包首部
            byte[] header = new byte[5];
            //包内容
            byte[] content = new byte[dataSize - 5];
            //包长度
            byte[] header_1 = new byte[4];

            Array.Copy(data, 0, header, 0, 5);
            Array.Copy(data, 5, content, 0, dataSize - 5);
            Array.Copy(data, 1, header_1, 0, 4);

            //包内容长度
            int dataLength = DataConverter.ByteArrayToInt32(header_1);

            //dataLength < content.Length 说明本次接收的数据中已经包含一个完整的包，将这个完整的包取出后继续处理剩下的数据
            if (dataLength < content.Length)
            {
                //发生了粘包
                byte[] b = new byte[dataLength];
                Array.Copy(content, 0, b, 0, dataLength);
                ReceiveDataModel receiveData = new ReceiveDataModel()
                {
                    DataType = header[0],
                    DataLength = dataLength,
                    Content = b
                };
                DataList.Add(receiveData);
                byte[] last = new byte[content.Length - dataLength];
                Array.Copy(content, dataLength, last, 0, last.Length);
                SplitData(last, last.Length);
            }
            //dataLength >= content.Length 说明本次接收的数据不完整，保存后继续接收
            else if (dataLength >= content.Length)
            {
                //发生了分包或者什么都没发生
                ReceiveDataModel receiveData = new ReceiveDataModel()
                {
                    DataType = header[0],
                    DataLength = dataLength,
                    Content = content
                };
                DataList.Add(receiveData);
                Offset = 0;
                Size = BufferLength;
                Buffer = new byte[BufferLength];
                if (dataLength == content.Length) IsComplete = true;
            }
        }

        public void Clear()
        {
            if (DataList.Count > 0)
            {
                DataList.Clear();
                IsComplete = false;
            }
        }
    }

    public struct ReceiveDataModel
    {
        /// <summary>
        /// 数据类型 0 文本，1 文件
        /// </summary>
        public byte DataType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Content { get; set; }

        public bool IsComplete
        {
            get
            {
                if (DataLength == 0) return false;
                return DataLength == Content.Length;
            }
        }
    }
}