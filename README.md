# EasyTcpSocket

这个项目是本人边学边做的，目标是先写一套TcpSocket框架，再以此为基础，搭建一个SECS/GEM框架，会持续更新，仅供参考。

**EasyTcpSocketTestClient**和**test_server**是我用这个框架写的测试客户端和测试服务端，目前一个服务端对应多个客户端同时收发消息没什么问题，也解决了粘包和分包的问题。

### 创建服务端
```
string serverIp = "xxx";
int serverPort = "xxx";
EasyTcpSocket.TcpSocketServer server = new EasyTcpSocket.TcpSocketServer(serverIp, serverPort, (string currentClientIp) =>
{
    //有新的客户端连接
}, (string clientIP, byte[] content, int length) =>
{
    //收到消息
    string str = Encoding.Default.GetString(content, 0, length);
});
server.Start();
```

### 创建客户端
```
string serverIp = "xxx";
int serverPort = xxx;
EasyTcpSocket.TcpSocketClient client = new TcpSocketClient((string serverIP, byte[] content, int length) =>
{
    //收到消息
    string str = Encoding.Default.GetString(content, 0, length);
});
if (client.Connect(serverIp, serverPort, out string errorMessage))
{
    //连接成功
}
else
{
    //连接失败
}
```
