using System;
using Microsoft.AspNetCore.Mvc;

using backend.Interfaces;
using backend.Models;

namespace backend.Controllers
{
    /// <summary>Asymmetric API encryption controller.</summary>
    [ApiController]
    [Route("[controller]")]
    public class AsymmetricController : ControllerBase
    {
        IAsymmetricCrypto crypto;
        /// <summary>Constructor taking a reference to an implementation of the <c>IAsymmetricCrypto</c> service interface.</summary>
        public AsymmetricController(IAsymmetricCrypto crypto)
        {
            this.crypto = crypto;
        }
        /// <summary>Generate a new key pair on the server and receive it as a pair of hex strings.</summary>
        [HttpGet]
        [Route("key")]
        public KeyPair GetKeyPair()
        {
            return crypto.GetKeys();
        }
        /// <summary>Get the current key pair in an OpenSSH-compatible format.</summary>
        [HttpGet]
        [Route("key/ssh")]
        public KeyPair GetKeyPairSsh()
        {
            return crypto.GetKeysFile();
        }
        /// <summary>Sign a message with the current key.</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /sign "It is I, LeClerc!"
        /// Sample response:
        ///     
        ///     KWzH1JzOKxgpPFtLW0U1YcgFqoEdMy/RLXoH8hGArQhf3zdeAOSc89o8kcv59eQ4SghPiM1ga5ydvoQPYpewTUCDMEXJDQ2f4bH4lZLxDk9L0I4XruWb2kPwOWORKnuMDVAzOniju3flTOtSg6iZYjVumuiEAaQtgpvKknFHUSs=
        /// </remarks>
        [HttpPost]
        [Route("sign")]
        public string Sign([FromBody] string message)
        {
            return crypto.Sign(message);
        }
        /// <summary>Verify the given message and signature.</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /verify {"message": "It is I, LeClerc!", "signature": "KWz(...)"}
        /// Sample response:
        ///
        ///     true
        /// </remarks>
        [HttpPost]
        [Route("verify")]
        public bool Verify([FromBody] SignatureClaim claim)
        {
            return crypto.Verify(claim.Message, claim.Signature);
        }
        /// <summary>Encrypts the message given.</summary>
        /// <returns>Ciphertext.</returns>
        [HttpPost]
        [Route("encode")]
        public string Encode([FromBody] string message)
        {
            return crypto.Encode(message);
        }
        /// <summary>Decrypts the ciphertext given.</summary>
        /// <returns>Plaintext.</returns>
        [HttpPost]
        [Route("decode")]
        public string Decode([FromBody] string message)
        {
            return crypto.Decode(message);
        }
    }
}
