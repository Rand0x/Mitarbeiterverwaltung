using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Helper
{
    public static class ByteStringConverter
    {
        public static string ToStringFromBytes(byte[] bytes)
        {
            var result = new StringBuilder();
            foreach (var elem in bytes)
            {
                result.Append(elem.ToString("x2"));
            }
            return result.ToString();
        }

        public static byte[] ToByteFromString(string bytes)
        {
            var result = new byte[bytes.Length / 2];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = Convert.ToByte(bytes.Substring(i * 2, 2), 16);
            }
            return result;
        }
    }
}
