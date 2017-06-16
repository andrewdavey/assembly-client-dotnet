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
    public class AssessmentsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<Assessment> assessments = new List<Assessment>()
            {
                new Assessment(), new Assessment()
            };

            Mock.Get(client).Setup(c => c.GetList<Assessment>(
                AssessmentsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(assessments));

            var resource = new AssessmentsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Assessment> assessments = new List<Assessment>()
            {
                new Assessment(), new Assessment()
            };

            Mock.Get(client).Setup(c => c.GetList<Assessment>(
                AssessmentsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(assessments)).Verifiable();

            var resource = new AssessmentsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(assessments.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}