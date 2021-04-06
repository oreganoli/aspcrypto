using System;
using System.Text.Json;
using Xunit;
using backend.Controllers;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace backend_tests
{
    public class SymmetricControllerFixture : IDisposable
    {
        public SymmetricController controller;
        public int num = 2;
        public SymmetricControllerFixture()
        {
            controller = new SymmetricController(new AesService());
        }

        public void Dispose()
        {
            return;
        }
    }
    public class SymmetricControllerTest : IClassFixture<SymmetricControllerFixture>
    {
        SymmetricControllerFixture fixture;
        public SymmetricControllerTest(SymmetricControllerFixture fixture)
        {
            this.fixture = fixture;
        }
        /// <summary>GetKey() should return a 200 OK response.</summary>
        [Fact]
        public void GetKeyIsOkay()
        {
            var ctr = fixture.controller;
            var result = ctr.GetKey();
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        /// <summary>SetKey() should return 204 on a valid key.</summary>
        [Fact]
        public void SetKeyValid()
        {
            var validKey = "2D-43-66-01-6E-B6-E7-6D-26-54-FD-67-D0-03-01-57-BF-3F-4F-30-6E-66-87-68-4A-20-54-53-58-04-EE-CE";
            var ctr = fixture.controller;
            var result = (NoContentResult)(ctr.SetKey(validKey));
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
        /// <summary>SetKey() should return 422 Unprocessable Entity on an invalid key.</summary>
        [Fact]
        public void SetKeyInvalid()
        {
            var invalidKey = "904127u0412cnadfn";
            var ctr = fixture.controller;
            var result = (JsonResult)(ctr.SetKey(invalidKey));
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, result.StatusCode);
        }

        /// <summary>Sanity check for encryption and decryption.</summary>
        [Fact]
        public void CryptoWorks()
        {
            var message = "The assassination is scheduled next week. I trust everyone will do their job.";
            var ctr = fixture.controller;
            ctr.GetKey();
            var encoded = (JsonResult)ctr.Encode(message);
            var ciphertext = encoded.Value.ToString();
            Assert.NotEqual(message, ciphertext);
            Assert.DoesNotContain("assassination", ciphertext);
            var decoded = (JsonResult)ctr.Decode(ciphertext);
            var plaintext = decoded.Value.ToString();
            Assert.Equal(message, plaintext);
        }
    }
}
