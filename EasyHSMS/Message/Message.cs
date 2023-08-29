using System.Net.Http.Headers;
using System.Xml;

namespace EasyHSMS
{
    public static class Message
    {
        public static byte[] CreateDataMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateSelectReqMessage(ushort sessionID)
        {
            return CreateMessage(sessionID, 0, 0, 0, 1, new byte[4] { 0, 0, 0, 0 }, null);
        }

        public static byte[] CreateSlectRspMessage(ushort sessionID, int selectStatus)
        {
            if (selectStatus < 0 || selectStatus > 255)
            {
                throw new ArgumentOutOfRangeException();
            }

            return CreateMessage(sessionID,);
        }

        public static byte[] CreateDeselectReqMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateDeselectRspMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateLinktestReqMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateLinktestRspMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateRejectReqMessage()
        {
            return new byte[0];
        }

        public static byte[] CreateSeparateReqMessage()
        {
            return new byte[0];
        }

        private static byte[] CreateMessage(ushort sessionID, byte byte2, byte byte3, byte pType, byte sType, byte[] systemBytes, string? content)
        {
            byte[] sessionIDBytes = DataConvert.ShortToBytes(sessionID);
            byte[] contentBytes = DataConvert.StringToBytes(content);

            int totalLength = 10 + contentBytes.Length;
            byte[] result = new byte[totalLength];

            Array.Copy(sessionIDBytes, 0, result, 0, 2);
            result[2] = byte2;
            result[3] = byte3;
            result[4] = pType;
            result[5] = sType;
            Array.Copy(systemBytes, 0, result, 6, 4);
            Array.Copy(contentBytes, 0, result, 10, contentBytes.Length + 10);

            return result;
        }
    }
}