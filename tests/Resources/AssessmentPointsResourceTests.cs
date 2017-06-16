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
    public class AssessmentPointsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<AssessmentPoint> assessmentPoints = new List<AssessmentPoint>()
            {
                new AssessmentPoint(), new AssessmentPoint()
            };

            Mock.Get(client).Setup(c => c.GetList<AssessmentPoint>(
                AssessmentPointsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(assessmentPoints));

            var resource = new AssessmentPointsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<AssessmentPoint> assessmentPoints = new List<AssessmentPoint>()
            {
                new AssessmentPoint(), new AssessmentPoint()
            };

            Mock.Get(client).Setup(c => c.GetList<AssessmentPoint>(
                AssessmentPointsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(assessmentPoints)).Verifiable();

            var resource = new AssessmentPointsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(assessmentPoints.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}