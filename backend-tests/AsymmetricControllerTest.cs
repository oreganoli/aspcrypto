using System;
using Xunit;
using backend.Controllers;
using backend.Models;
using backend.Services;

namespace backend_tests
{
    public class AsymmetricControllerFixture
    {
        public AsymmetricController controller { get; }
        public AsymmetricControllerFixture()
        {
            var svc = new RsaService();
            controller = new AsymmetricController(svc);
        }
    }
    public class AsymmetricControllerTest : IClassFixture<AsymmetricControllerFixture>
    {
        public AsymmetricControllerFixture fixture;

        public AsymmetricControllerTest(AsymmetricControllerFixture fixture)
        {
            this.fixture = fixture;
        }
        /// <summary>Sanity check for key getting and setting - the hex string format should be compatible with both.</summary>
        [Fact]
        public void KeySetGet()
        {
            var ctr = fixture.controller;
            var pair = ctr.GetKeyPair();
            ctr.SetKeyPair(pair);
        }

        /// <summary>Sanity check for encryption and decryption.</summary>
        [Fact]
        public void CryptoWorks()
        {
            var message = "The assassination is scheduled next week. I trust everyone will do their job.";
            var ctr = fixture.controller;
            var pair = ctr.GetKeyPair();
            var encoded = ctr.Encode(message);
            Assert.NotEqual(message, encoded);
            Assert.DoesNotContain("assassination", encoded);
            var decoded = ctr.Decode(encoded);
            Assert.Equal(message, decoded);
        }
        /// <summary>Signing and verification test.</summary>
        [Fact]
        public void SignVerifyTest()
        {
            var ctr = fixture.controller;
            var message1 = "It is I, LeClerc!";
            var message2 = "Listen very carefully, I shall say this only once!";
            var signature1 = ctr.Sign(message1);
            Assert.True(ctr.Verify(new SignatureClaim(message1, signature1)));
            Assert.False(ctr.Verify(new SignatureClaim(message2, signature1)));
            // LeClerc can't impersonate anyone
        }
    }
}