using System;
using Microsoft.AspNetCore.Mvc;
namespace backend.Models
{
    /// <summary>Struct representing an asymmetric key pair in hex string or OpenSSH-compatible PEM form.</summary>
    public struct KeyPair
    {
        /// <summary>Public key.</summary>
        public string PublicKey { get; }
        /// <summary>Private key.</summary>
        public string PrivateKey { get; }

        /// <summary>Constructor. Self-explanatory.</summary>
        public KeyPair(string publicKey, string privateKey)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
    }
}