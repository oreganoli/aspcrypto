using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using backend.Interfaces;
namespace backend.Services
{
    /// <summary>An implementation of <c>ISymmetricCrypto</c> using the AES algorithm.</summary>
    class AesService : ISymmetricCrypto
    {
        Aes crypto;
        public AesService()
        {
            crypto = Aes.Create();
            crypto.GenerateKey();
            crypto.GenerateIV();
        }
        /// <summary>Decodes ciphertext.</summary>
        /// <param name="msg">Ciphertext encoded as Base64.</param>
        /// <returns>Plaintext.</returns>
        public string Decode(string msg)
        {
            byte[] inputBytes = Convert.FromBase64String(msg);
            string output = "";
            var decryptor = crypto.CreateDecryptor();
            using (var memStream = new MemoryStream(inputBytes))
            {
                using (var crStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(crStream))
                    {
                        output = reader.ReadToEnd();
                    }
                }
            }
            return output;
        }
        /// <summary>Encodes plaintext.</summary>
        /// <param name="msg">Plaintext.</param>
        /// <returns>Ciphertext in Base64 format.</returns>
        public string Encode(string msg)
        {
            byte[] outputBytes;
            var encryptor = crypto.CreateEncryptor();
            using (var memStream = new MemoryStream())
            {
                using (var crStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(crStream))
                    {
                        writer.Write(msg);
                    }
                    outputBytes = memStream.ToArray();
                }
            }
            return Convert.ToBase64String(outputBytes);
        }
        /// <summary>Generates a new symmetric encryption key.</summary>
        /// <returns>The generated key.</returns>
        public string GetKey()
        {
            crypto.GenerateKey();
            crypto.GenerateIV();
            var keyBytes = crypto.Key;
            return BitConverter.ToString(keyBytes); // Convert to hex string
        }
        /// <summary>Sets the symmetric encryption key.</summary>
        public void SetKey(byte[] key)
        {
            crypto.Key = key;
        }
    }
}
