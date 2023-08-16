using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTcpSocket.Common
{
    public static class DataConverter
    {
        /// <summary>
        /// 接受一个 int32 类型的参数，返回一个长度为 4 的 byte 数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Int32ToByteArray(int value)
        {
            byte[] result = new byte[4];
            result[0] = (byte)(value >> 24); // 存储最高位字节
            result[1] = (byte)(value >> 16); // 存储次高位字节
            result[2] = (byte)(value >> 8);  // 存储次低位字节
            result[3] = (byte)value;         // 存储最低位字节

            return result;
        }

        /// <summary>
        /// 接受一个长度为 4 的 byte 数组，返回一个 int32 类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ByteArrayToInt32(byte[] value)
        {
            if (value.Length != 4)
            {
                throw new ArgumentException("Byte array must have length of 4.");
            }

            int result = 0;
            result |= value[0] << 24; // 还原最高位字节
            result |= value[1] << 16; // 还原次高位字节
            result |= value[2] << 8;  // 还原次低位字节
            result |= value[3];       // 还原最低位字节

            return result;
        }
    }
}
