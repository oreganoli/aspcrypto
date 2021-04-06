using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            crypto.GenerateKey();
            crypto.GenerateIV();
        }
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

        public string GetKey()
        {
            crypto.GenerateKey();
            crypto.GenerateIV();
            var keyBytes = crypto.Key;
            return BitConverter.ToString(keyBytes); // Convert to hex string
        }

        public void SetKey(byte[] key)
        {
            crypto.Key = key;
        }
    }
}
