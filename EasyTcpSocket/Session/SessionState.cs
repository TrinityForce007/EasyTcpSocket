using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTcpSocket
{
    public enum SessionState
    {
        None = 0,

        Initialized = 1,

        Connected = 2,

        Closed = 3
    }
}
