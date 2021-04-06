using System;
using Xunit;
using backend.Controllers;
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
    }
}