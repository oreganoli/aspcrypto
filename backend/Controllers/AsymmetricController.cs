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
    }
}
