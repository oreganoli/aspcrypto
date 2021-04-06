namespace backend.Models
{
    /// <summary>Struct representing an asymmetric key pair.</summary>
    public struct KeyPair
    {
        /// <summary>Public key as a hex string.</summary>
        public string publicKey;
        /// <summary>Private key as a hex string.</summary>
        public string privateKey;

        /// <summary>Constructor. Self-explanatory.</summary>
        public KeyPair(string publicKey, string privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }
    }
}