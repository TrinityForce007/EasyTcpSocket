using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTcpSocket
{
    public delegate void Event_ReceivedMessage(string clientIP, byte[] message, int length);
    public delegate void Event_NewClientConncected(List<string> clientIpList, string currentClientIp);


    public class Delegates
    {
    }
}
