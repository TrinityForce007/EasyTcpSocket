using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EasyTcpSocket
{
    public class SessionContainer
    {
        private ConcurrentDictionary<string, IAppSession> _sessions;

        public SessionContainer()
        {
            _sessions = new ConcurrentDictionary<string, IAppSession>(StringComparer.OrdinalIgnoreCase);
        }


        public ValueTask<bool> RegisterSession(IAppSession session)
        {
            _sessions.TryAdd(session.SessionID, session);
            return new ValueTask<bool>(true);
        }

        public ValueTask<bool> UnRegisterSession(IAppSession session)
        {
            _sessions.TryRemove(session.SessionID, out IAppSession removedSession);
            return new ValueTask<bool>(true);
        }

        public IAppSession GetSessionByID(string sessionID)
        {
            _sessions.TryGetValue(sessionID, out IAppSession session);
            return session;
        }

        public int GetSessionCount()
        {
            return _sessions.Count;
        }
    }
}
