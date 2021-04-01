using System;
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
            symmetricService.SetKey(keyBytes);
            return new NoContentResult();
        }
    }
}