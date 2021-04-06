using System;
using System.Collections.Generic;
using System.Linq;
namespace backend.Common
{
    /// <summary>Helper class for stripping hexadecimal strings of any extraneous characters and converting them to bytes.</summary>
    public static class HexStr
    {
        /// <summary>Transform a hex string into a byte array after stripping it of any unnecessary characters.</summary>
        /// <param name="input">The hex string to be converted to bytes</param>
        /// <returns>Byte array corresponding to the hex string</returns>
        public static byte[] ToBytes(string input)
        {
            var input_sanitized = new string(input.Where(c => "0123456789ABCDEFabcdef".Contains(c)).ToArray()).ToUpperInvariant();
            return Convert.FromHexString(input_sanitized);
        }
    }
}