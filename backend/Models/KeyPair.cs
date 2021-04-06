using System;
using Microsoft.AspNetCore.Mvc;
namespace backend.Models
{
    /// <summary>Struct representing an asymmetric key pair.</summary>
    public struct KeyPair
    {
        /// <summary>Public key as a hex string.</summary>
        public string PublicKey { get; }
        /// <summary>Private key as a hex string.</summary>
        public string PrivateKey { get; }

        /// <summary>Constructor. Self-explanatory.</summary>
        public KeyPair(string publicKey, string privateKey)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
    }
}