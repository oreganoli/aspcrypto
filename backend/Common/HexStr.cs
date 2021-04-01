using System;
using System.Collections.Generic;
using System.Linq;
namespace backend.Common
{
    public static class HexStr
    {
        public static byte[] ToBytes(string input)
        {
            var input_sanitized = input.Where(c => "0123456789ABCDEFabcdef".Contains(c)).ToArray().ToString();
            var bytes = new byte[input_sanitized.Length / 2];
            for (int i = 0; i < bytes.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(input_sanitized.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}