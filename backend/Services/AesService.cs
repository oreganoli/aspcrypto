using System;
using System.Security.Cryptography;
using backend.Interfaces;
/// <summary>An implementation of <c>ISymmetricCrypto</c> using the AES algorithm.</summary>
namespace backend.Services
{
    class AesService : ISymmetricCrypto
    {
        Aes crypto;
        public AesService()
        {
            crypto = Aes.Create();
        }
        public string Decode(string msg)
        {
            throw new System.NotImplementedException();
        }

        public string Encode(string msg)
        {
            throw new System.NotImplementedException();
        }

        public string GetKey()
        {
            crypto.GenerateKey();
            var keyBytes = crypto.Key;
            return BitConverter.ToString(keyBytes); // Convert to hex string
        }

        public void SetKey(byte[] key)
        {
            crypto.Key = key;
        }
    }
}
