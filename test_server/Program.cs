using System.Text;

namespace test_server
{
    public class Program
    {
        private static int index = 0;
        private static EasyTcpSocket.TcpSocketServer Server;
        private static object syncRoot = new object();

        private static void Main(string[] args)
        {
            Server = new EasyTcpSocket.TcpSocketServer("127.0.0.1", 3333, Event_NewClientConncected, Event_ReceivedMessage);
            Server.Start();
            Console.WriteLine("开始监听");
            Console.ReadLine();
        }

        private static void Event_NewClientConncected(List<string> clientIpList, string currentClientIp)
        {
            Console.WriteLine($"新连接：{currentClientIp}");
        }

        private static void Event_ReceivedMessage(string clientIP, byte[] content, int length)
        {
            lock (syncRoot)
            {
                index++;
                string str = Encoding.Default.GetString(content, 0, length);
                Console.WriteLine($"{index} {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} 收到{clientIP}的消息：{str}");

                //将消息原封不动的发回去
                Thread.Sleep(10);
                Server.Send(clientIP, EasyTcpSocket.DataType.Message, content);
            }
        }
    }
}