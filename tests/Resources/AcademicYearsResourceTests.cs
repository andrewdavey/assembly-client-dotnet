using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class AcademicYearsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<AcademicYear> groups = new List<AcademicYear>()
            {
                new AcademicYear(), new AcademicYear()
            };

            Mock.Get(client).Setup(c => c.GetList<AcademicYear>(AcademicYearsResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(groups));

            var resource = new AcademicYearsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<AcademicYear> academicYears = new List<AcademicYear>()
            {
                new AcademicYear(), new AcademicYear()
            };

            Mock.Get(client).Setup(c => c.GetList<AcademicYear>(
                AcademicYearsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(academicYears)).Verifiable();

            var resource = new AcademicYearsResource(client);
            var results = await resource.List();
            Assert.That(results.Count, Is.EqualTo(academicYears.Count));

            Mock.Get(client).VerifyAll();
        }
   }
}