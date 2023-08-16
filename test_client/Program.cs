using System.Text;

namespace test_client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EasyTcpSocket.TcpSocketClient client = new EasyTcpSocket.TcpSocketClient("127.0.0.1", 3333, Event_ReceivedMessage);
            
            if (client.Connect(out string errorMessage))
            {
                Console.WriteLine("连接成功");
                Console.WriteLine("输入内容回车发送");

                while (true)
                {
                    string str = Console.ReadLine();
                    byte[] data = Encoding.Default.GetBytes(str.ToCharArray());

                    //测试粘包
                    for (int i = 0; i < 1000; i++)
                    {
                        client.Send(EasyTcpSocket.DataType.Message, data);
                    }
                }
            }
            else
            {
                Console.WriteLine("连接失败," + errorMessage);
                Console.ReadLine();
            }
        }

        static void Event_ReceivedMessage(string clientIP, byte[] content, int length)
        {
            string str = Encoding.Default.GetString(content, 0, length);
            Console.WriteLine("收到消息：" + str);
        }

    }
}