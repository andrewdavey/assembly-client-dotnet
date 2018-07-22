using System.Dynamic;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class AspectsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<Aspect> aspects = new List<Aspect>()
            {
                new Aspect(), new Aspect()
            };

            Mock.Get(client).Setup(c => c.GetList<Aspect>(AspectsResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(aspects));

            var resource = new AspectsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Aspect> aspects = new List<Aspect>()
            {
                new Aspect(), new Aspect()
            };

            Mock.Get(client).Setup(c => c.GetList<Aspect>(
                AspectsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(aspects)).Verifiable();

            var resource = new AspectsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(aspects.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}