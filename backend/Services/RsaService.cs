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
    /// <summary>An implementation of <c>IAsymmetricCrypto</c> using the RSA algorithm.</summary>
    public class RsaService : IAsymmetricCrypto
    {
        RSACryptoServiceProvider rsa;
        /// <summary>Constructor.</summary>
        public RsaService()
        {
            rsa = new RSACryptoServiceProvider();
        }
        /// <summary>Decode the given ciphertext.</summary>
        public string Decode(string message)
        {
            var inputBytes = Convert.FromBase64String(message);
            var outputBytes = rsa.Decrypt(inputBytes, true);
            return Encoding.UTF8.GetString(outputBytes);
        }
        /// <summary>Encode the given plaintext.</summary>
        public string Encode(string message)
        {
            var inputBytes = Encoding.UTF8.GetBytes(message);
            var outputBytes = rsa.Encrypt(inputBytes, true);
            return Convert.ToBase64String(outputBytes);
        }
        /// <summary>Regenerate the asymmetric service's keys and return the pair in hex string format.</summary>
        /// <returns>Hex string variant of <c>KeyPair</c></returns>
        public KeyPair GetKeys()
        {
            return new KeyPair(BitConverter.ToString(rsa.ExportRSAPublicKey()), BitConverter.ToString(rsa.ExportRSAPrivateKey()));
        }
        /// <summary>Regenerate the asymmetric service's keys and return the pair in PEM file format.</summary>
        /// <returns>PEM file variant of <c>KeyPair</c></returns>
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
        /// <summary>Set key pair to the given one.</summary>
        /// <param name="pair"><c>KeyPair</c> in hex string format, like that returned by <c>GetKeys()</c></param>
        public void SetKeys(KeyPair pair)
        {
            rsa.ImportRSAPrivateKey(HexStr.ToBytes(pair.PrivateKey), out var dummyPriv);
            rsa.ImportRSAPublicKey(HexStr.ToBytes(pair.PublicKey), out var dummyPub);
        }
        /// <summary>Produce a signature for the message given.</summary>
        public string Sign(string message)
        {
            var inputBytes = Encoding.UTF8.GetBytes(message);
            var signatureBytes = rsa.SignData(inputBytes, SHA256.Create());
            return Convert.ToBase64String(signatureBytes);
        }
        /// <summary>Given a message and a claimed signature for it, perform verification.</summary>
        public bool Verify(string message, string signature)
        {
            var dataBytes = Encoding.UTF8.GetBytes(message);
            var signatureBytes = Convert.FromBase64String(signature);
            return rsa.VerifyData(dataBytes, SHA256.Create(), signatureBytes);
        }
    }
}