namespace backend.Interfaces
{
    /// <summary>Interface for services offering symmetric cryptography.</summary>
    public interface ISymmetricCrypto
    {
        /// <summary>Generates and returns a new key as a hexadecimal string, also setting it for future use.</summary>
        public string GetKey();
        /// <summary>Sets a new key.</summary>
        public void SetKey(byte[] key);
        /// <summary>Encodes a string using the currently set key and returns it as base64.</summary>
        /// <param name="msg">Message to be encoded.</param>
        public string Encode(string msg);
        /// <summary>Decodes a string with the currently set key.</summary>
        /// <param name="msg">Message to be decoded.</param>
        public string Decode(string msg);
    }
}