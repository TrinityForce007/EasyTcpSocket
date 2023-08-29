using System.Net;
using System.Net.Sockets;

namespace EasyTcpSocket
{
    public class AppSession : IAppSession
    {
        public string SessionID { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset LastActiveTime { get; private set; }
        public SessionState State { get; private set; } = SessionState.None;
        public Socket SocketObj { get; private set; }
        public EndPoint RemoteEndPoint { get { return SocketObj.RemoteEndPoint; } }

        public AppSession(Socket socket)
        {
            SessionID = Guid.NewGuid().ToString();
            StartTime = DateTimeOffset.Now;
            State = SessionState.Initialized;
            SocketObj = socket;
        }

        public void Reset()
        {
            State = SessionState.None;
            StartTime = default(DateTimeOffset);
            LastActiveTime = default(DateTimeOffset);
        }
    }
}