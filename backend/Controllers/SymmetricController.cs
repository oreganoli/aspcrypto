using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Common;
using backend.Interfaces;
namespace backend.Controllers
{
    /// <summary>Symmetric encryption API controller.</summary>
    [ApiController]
    [Route("[controller]")]
    public class SymmetricController : ControllerBase
    {
        ISymmetricCrypto symmetricService;
        /// <summary>Constructor taking a reference to an <c>ISymmetricCrypto</c> instance.</summary>
        /// <param name="symmetricService">A symmetric cryptography service implementing the <c>ISymmetricCrypto</c> interface.</param>
        public SymmetricController(ISymmetricCrypto symmetricService)
        {
            this.symmetricService = symmetricService;
        }
        /// <summary>Generates a new key on the backend and returns it.</summary>
        /// <returns>New key as a JSON string</returns>
        [HttpGet]
        [Route("key")]
        public JsonResult GetKey()
        {
            var key = symmetricService.GetKey();
            var result = new JsonResult(key);
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }
        /// <summary>Sets the symmetric key to the one provided as a JSON string in the body.</summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /symmetric/key/ "2D-43-66-01-6E-B6-E7-6D-26-54-FD-67-D0-03-01-57-BF-3F-4F-30-6E-66-87-68-4A-20-54-53-58-04-EE-CE"
        /// </remarks>
        /// <returns>Nothing on success.</returns>
        /// <response code="204">The key was successfully set.</response>
        /// <response code="522">The key was invalid and could not be set.</response>
        [HttpPost]
        [Route("key")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult SetKey([FromBody] string key)
        {
            var keyBytes = HexStr.ToBytes(key);
            try
            {
                symmetricService.SetKey(keyBytes);
            }
            catch (CryptographicException ex)
            {
                var error_desc = new
                {
                    ErrorCode = StatusCodes.Status422UnprocessableEntity,
                    Message = ex.Message
                };
                var result = new JsonResult(error_desc);
                result.StatusCode = error_desc.ErrorCode;
                return result;
            }
            return new NoContentResult();
        }
        /// <summary>Encodes plaintext given as a JSON string, returning a Base64 representation of the ciphertext.</summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /symmetric/encode/ "Tomorrow we detonate the bombs"
        ///
        /// Sample response:
        ///
        ///     "efUPUviqVTP3Ja4UjqYrR9PSpm7Zik6BnNjsvp7TNcY="
        ///
        /// </remarks>
        /// <returns>Base64 string representing the encoded plaintext.</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [Route("encode")]
        public IActionResult Encode([FromBody] string msg)
        {
            try
            {
                var encoded = symmetricService.Encode(msg);
                return new JsonResult(encoded);
            }
            catch (Exception e)
            {
                var error_desc = new
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Message = e.Message
                };
                var result = new JsonResult(error_desc);
                result.StatusCode = error_desc.ErrorCode;
                return result;
            }
        }
        /// <summary>Decodes Base64 ciphertext given as a JSON string, returning the decrypted plaintext.</summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /symmetric/decode/ "efUPUviqVTP3Ja4UjqYrR9PSpm7Zik6BnNjsvp7TNcY="
        ///
        /// Sample response:
        ///
        ///     "Tomorrow we detonate the bombs"
        ///
        /// </remarks>
        /// <returns>Decrypted plaintext.</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [Route("decode")]
        public IActionResult Decode([FromBody] string msg)
        {
            try
            {
                var decoded = symmetricService.Decode(msg);
                return new JsonResult(decoded);
            }
            catch (Exception e)
            {
                var error_desc = new
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Message = e.Message
                };
                var result = new JsonResult(error_desc);
                result.StatusCode = error_desc.ErrorCode;
                return result;
            }
        }
    }
}