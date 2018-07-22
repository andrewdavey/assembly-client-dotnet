using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class StaffMembersResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<StaffMember> mockResults = new List<StaffMember>()
            {
                new StaffMember(), new StaffMember()
            };

            Mock.Get(client).Setup(c => c.GetList<StaffMember>(StaffMembersResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(mockResults));

            var resource = new StaffMembersResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<StaffMember> mockResults = new List<StaffMember>()
            {
                new StaffMember(), new StaffMember()
            };

            DateTime date = DateTime.Now;
            var dateTimeString = date.ToString(Resource.DateFormat);

            Mock.Get(client).Setup(c => c.GetList<StaffMember>(
                StaffMembersResource.ResourceName,
                It.Is<ExpandoObject>(x =>
                x.V<string>("date") == dateTimeString &&
                x.V<bool>("teachers_only") == true))).Returns(Task.FromResult(mockResults)).Verifiable();

            var resource = new StaffMembersResource(client);
            var results = await resource.List(date: date, teachersOnly: true);
            Assert.That(results.Count, Is.EqualTo(mockResults.Count));

            Mock.Get(client).VerifyAll();
        }
    }
}
