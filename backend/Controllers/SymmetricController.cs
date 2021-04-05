using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Common;
using backend.Interfaces;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SymmetricController : ControllerBase
    {
        static readonly string JSON_HEADER = "application/json";
        ISymmetricCrypto symmetricService;
        public SymmetricController(ISymmetricCrypto symmetricService)
        {
            this.symmetricService = symmetricService;
        }
        [HttpGet]
        [Route("key")]
        public IActionResult GetKey()
        {
            Response.ContentType = JSON_HEADER;
            var key = symmetricService.GetKey();
            return new JsonResult(key);
        }
        [HttpPost]
        [Route("key")]
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
    }
}