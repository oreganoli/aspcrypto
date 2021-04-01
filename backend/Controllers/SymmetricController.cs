using System;
using Microsoft.AspNetCore.Mvc;
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
    }
}