using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTcpSocket.Event
{
    public class ReceivedEventArgs : EventArgs
    {
        public string SessionID { get; private set; }

        public byte[] Data { get; private set; }

        public ReceivedEventArgs(string sessionID, byte[] data)
        {
            SessionID = sessionID;
            Data = data;
        }
    }
}
