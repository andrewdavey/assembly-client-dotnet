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
    public class GradeSetsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<GradeSet> gradeSets = new List<GradeSet>()
            {
                new GradeSet(), new GradeSet()
            };

            Mock.Get(client).Setup(c => c.GetList<GradeSet>(
                GradeSetsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(gradeSets));

            var resource = new GradeSetsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<GradeSet> gradeSets = new List<GradeSet>()
            {
                new GradeSet(), new GradeSet()
            };

            Mock.Get(client).Setup(c => c.GetList<GradeSet>(
                GradeSetsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(gradeSets)).Verifiable();

            var resource = new GradeSetsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(gradeSets.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}