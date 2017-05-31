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
    public class SubjectsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<Subject> subjects = new List<Subject>()
            {
                new Subject(), new Subject()
            };

            Mock.Get(client).Setup(c => c.GetList<Subject>(SubjectsResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(subjects));

            var resource = new SubjectsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Subject> subjects = new List<Subject>()
            {
                new Subject(), new Subject()
            };

            Mock.Get(client).Setup(c => c.GetList<Subject>(
                SubjectsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(subjects)).Verifiable();

            var resource = new SubjectsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(subjects.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}