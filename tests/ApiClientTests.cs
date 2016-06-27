using System;
using NUnit.Framework;
using AssemblyClient;

namespace AllTheTests
{
    [TestFixture]
    public class SomeTests
    {
        [Test]
        public void AcceptsTheConfiguration()
        {
            var token = new Random().Next().ToString();
            var refreshToken = new Random().Next().ToString();
            var clientId = new Random().Next().ToString();
            var clientSecret = new Random().Next().ToString();

            var config = new ApiConfiguration
            {
                Token = token,
                RefreshToken = refreshToken,
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            var client = new ApiClient();
            client.Configure(config);

            Assert.That(client.Configuration.Token, Is.EqualTo(token));
            Assert.That(client.Configuration.RefreshToken, Is.EqualTo(refreshToken));
            Assert.That(client.Configuration.ClientId, Is.EqualTo(clientId));
            Assert.That(client.Configuration.ClientSecret, Is.EqualTo(clientSecret));
        }
    }
}