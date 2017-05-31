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
    public class SchoolDetailsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestSchoolDetailsFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var school = new School();

            Mock.Get(client).Setup(c => c.GetObject<School>(
                SchoolDetailsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(school)).Verifiable();

            var schoolDetailsResource = new SchoolDetailsResource(client);
            var result = await schoolDetailsResource.Details();

            Mock.Get(client).VerifyAll();
        }
    }
}