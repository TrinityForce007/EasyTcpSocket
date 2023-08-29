namespace EasyTcpSocket.Event
{
    public class ConnectedEventArgs : EventArgs
    {
        public IAppSession Session { get; private set; }

        public ConnectedEventArgs(IAppSession appSession)
        {
            Session = appSession;
        }
    }
}