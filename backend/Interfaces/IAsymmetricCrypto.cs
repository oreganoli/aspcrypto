using backend.Models;

namespace backend.Interfaces
{
    /// <summary>Interface for services offering asymmetric cryptography.</summary>
    public interface IAsymmetricCrypto
    {
        /// <summary>Generate a new key pair and set it as the current one, returning it as a <c>KeyPair</c> of hex strings.</summary>
        public KeyPair GetKeys();
        /// <summary>Returns the key pair in a standardized plaintext format.</summary>
        public string GetKeysFile();
        /// <summary>Sets the keys on the server.</summary>
        public string SetKeys(KeyPair pair);
        /// <summary>Verifies whether or not the message given produces the signature given with the keys currently in use.</summary>
        public bool Verify(string message, string signature);
        /// <summary>Produces a signature for the given message.</summary>
        public string Sign(string message);
        /// <summary>Encodes the given plaintext using the current key.</summary>
        public string Encode(string message);
        /// <summary>Decodes the given plaintext using the current key.</summary>
        public string Decode(string message);
    }
}