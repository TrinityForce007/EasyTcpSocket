using System.Text;

namespace test_server
{
    internal class Program
    {
        static string clientIp = string.Empty;
        static int index = 0;
        static void Main(string[] args)
        {
            EasyTcpSocket.TcpSocketServer server = new EasyTcpSocket.TcpSocketServer("127.0.0.1", 3333, Event_NewClientConncected, Event_ReceivedMessage);
            server.StartListen();
            Console.WriteLine("开始监听");
            Console.WriteLine("输入内容回车发送");
            while (true)
            {
                string str = Console.ReadLine();
                byte[] data = Encoding.Default.GetBytes(str.ToCharArray());
                server.Send(clientIp, EasyTcpSocket.DataType.Message, data);
            }
        }

        static void Event_NewClientConncected(List<string> clientIpList, string currentClientIp)
        {
            clientIp = currentClientIp;
            Console.WriteLine($"新连接：{currentClientIp}");
        }

        static void Event_ReceivedMessage(string clientIP, byte[] content, int length)
        {
            index++;
            string str = Encoding.Default.GetString(content, 0, length);
            Console.WriteLine($"{index} 收到{clientIP}的消息：{str}");
        }
    }
}

