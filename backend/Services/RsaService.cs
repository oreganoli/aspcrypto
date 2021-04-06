using System;
using System.Security.Cryptography;
using System.Text;
using backend.Common;
using backend.Interfaces;
using backend.Models;

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

        public string GetKeysFile()
        {
            throw new NotImplementedException();
        }

        public void SetKeys(KeyPair pair)
        {
            rsa.ImportRSAPrivateKey(HexStr.ToBytes(pair.privateKey), out var dummyPriv);
            rsa.ImportRSAPublicKey(HexStr.ToBytes(pair.publicKey), out var dummyPub);
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