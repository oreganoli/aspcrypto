namespace backend.Models
{
    /// <summary>Struct representing an asymmetric key pair.</summary>
    public struct KeyPair
    {
        string publicKey;
        string privateKey;

        /// <summary>Constructor. Self-explanatory.</summary>
        public KeyPair(string publicKey, string privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }
    }
}