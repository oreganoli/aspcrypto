using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using backend.Common;
using backend.Interfaces;
using backend.Models;
using PemUtils;


namespace backend.Services
{
    class RsaService : IAsymmetricCrypto
    {
        RSACryptoServiceProvider rsa;
        public RsaService()
        {
            rsa = new RSACryptoServiceProvider();
        }
        public string Decode(string message)
        {
            var inputBytes = Convert.FromBase64String(message);
            var outputBytes = rsa.DecryptValue(inputBytes);
            return Encoding.UTF8.GetString(outputBytes);
        }

        public string Encode(string message)
        {
            var inputBytes = Encoding.UTF8.GetBytes(message);
            var outputBytes = rsa.EncryptValue(inputBytes);
            return Convert.ToBase64String(outputBytes);
        }

        public KeyPair GetKeys()
        {
            return new KeyPair(BitConverter.ToString(rsa.ExportRSAPublicKey()), BitConverter.ToString(rsa.ExportRSAPrivateKey()));
        }

        public KeyPair GetKeysFile()
        {
            byte[] privateKeyBytes;
            byte[] publicKeyBytes;
            using (var privMemStream = new MemoryStream())
            using (var writer = new PemWriter(privMemStream))
            {
                writer.WritePrivateKey(rsa);
                privateKeyBytes = privMemStream.ToArray();
            }
            using (var pubMemStream = new MemoryStream())
            using (var writer = new PemWriter(pubMemStream))
            {
                writer.WritePublicKey(rsa);
                publicKeyBytes = pubMemStream.ToArray();
            }
            string privateKey = Encoding.UTF8.GetString(privateKeyBytes);
            string publicKey = Encoding.UTF8.GetString(publicKeyBytes);
            return new KeyPair(publicKey, privateKey);
        }

        public void SetKeys(KeyPair pair)
        {
            rsa.ImportRSAPrivateKey(HexStr.ToBytes(pair.PrivateKey), out var dummyPriv);
            rsa.ImportRSAPublicKey(HexStr.ToBytes(pair.PublicKey), out var dummyPub);
        }

        public string Sign(string message)
        {
            var inputBytes = Encoding.UTF8.GetBytes(message);
            var signatureBytes = rsa.SignData(inputBytes, SHA256.Create());
            return Convert.ToBase64String(signatureBytes);
        }

        public bool Verify(string message, string signature)
        {
            var dataBytes = Encoding.UTF8.GetBytes(message);
            var signatureBytes = Convert.FromBase64String(signature);
            return rsa.VerifyData(dataBytes, SHA256.Create(), signatureBytes);
        }
    }
}