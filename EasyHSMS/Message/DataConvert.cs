using System;
using System.Collections;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace EasyHSMS
{
    public static class DataConvert
    {
        public static byte[] IntToBytes(int value)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(value >> 24);
            result[1] = (byte)(value >> 16);
            result[2] = (byte)(value >> 8);
            result[3] = (byte)value;
            return result;
        }

        public static int BytesToInt(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("Byte array must contain exactly 4 elements.");
            }

            int result = 0;
            result |= (int)bytes[0] << 24;
            result |= (int)bytes[1] << 16;
            result |= (int)bytes[2] << 8;
            result |= (int)bytes[3];
            return result;
        }

        public static byte[] ShortToBytes(ushort value)
        {
            byte[] result = new byte[2];
            result[0] = (byte)(value >> 8);
            result[1] = (byte)value;
            return result; 
        }

        public static ushort BytesToShort(byte[] bytes)
        {
            if (bytes.Length != 2)
            {
                throw new ArgumentException("Byte array must contain exactly 2 elements.");
            }

            ushort result = 0;
            result |= (ushort)(bytes[0] << 8);
            result |= bytes[1];
            return result;
        }

        public static byte[] StringToBytes(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return Array.Empty<byte>();
            }

            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}