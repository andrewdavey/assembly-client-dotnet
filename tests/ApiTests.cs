using System;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class ApiTests
    {
        HttpClient client;

        [SetUp]
        public void Setup()
        {
            var handler = new Mock<HttpMessageHandler>();

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }))
                .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
                {
                    Assert.AreEqual(HttpMethod.Get, r.Method);
                });

            client = new HttpClient(handler.Object) 
            {
                BaseAddress = new System.Uri("http://test.lvh.me")
            };
        }
        [Test]
        public void ShouldThrowAnExceptionIfTheResponseIsNotOk()
        {
            var api = new Api(client);
            var config = new ApiConfiguration();
            Assert.Throws<HttpRequestException>(() => api.get<Student>("students", config));
        }
    }
}