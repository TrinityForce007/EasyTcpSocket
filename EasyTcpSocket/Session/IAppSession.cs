using System.Net;
using System.Net.Sockets;

namespace EasyTcpSocket
{
    public interface IAppSession
    {
        public string SessionID { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset LastActiveTime { get; }
        public SessionState State { get; }
        public Socket SocketObj { get; }

        public EndPoint RemoteEndPoint { get; }

        public void Reset()
        {
        }
    }
}