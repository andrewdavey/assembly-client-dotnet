using System;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class YearGroupsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<YearGroup> groups = new List<YearGroup>()
            {
                new YearGroup(), new YearGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<YearGroup>(YearGroupsResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(groups));

            var resource = new YearGroupsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<YearGroup> yearGroups = new List<YearGroup>()
            {
                new YearGroup(), new YearGroup()
            };

            var yearCode = "a";

            Mock.Get(client).Setup(c => c.GetList<YearGroup>(
                YearGroupsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(yearGroups)).Verifiable();

            var resource = new YearGroupsResource(client);
            var results = await resource.List(yearCode: yearCode);
            Assert.That(results.Count, Is.EqualTo(yearGroups.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestAnItemFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new YearGroup();

            int itemId = 1;

            Mock.Get(client).Setup(c => c.GetObject<YearGroup>(
                $"{YearGroupsResource.ResourceName}/{itemId}",
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(item)).Verifiable();

            var yearGroupResource = new YearGroupsResource(client);
            var result = await yearGroupResource.Find(itemId);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShoudFetchAYearGroup()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://test.lvh.me/year-group/1")
                .Respond(HttpStatusCode.OK, "application/json", "{ 'object': 'registration_group', 'id': 1, 'name': 'NAME', 'supervisor_ids': [ 1 ], 'student_ids': [ 15, 50, 109 ] } ");

            var client = new HttpClient(mockHttp)
            {
                BaseAddress = new System.Uri("http://test.lvh.me")
            };

            var api = new Api(client);
            var config = new ApiConfiguration();
            api.Configuration = config;

            var emptyArgs = new ExpandoObject();
            Action<string> refreshHandler = (token) => { };

            var result = await api.GetObject<YearGroup>("year-group/1", emptyArgs);

            Assert.That(result.Name, Is.EqualTo("NAME"));
        }

        [Test]
        public async Task YearGroupFetchesStudents()
        {
            IList<Student> groups = new List<Student>()
            {
                new Student(), new Student()
            };

            var client = Mock.Of<ApiClient>();

            var resource = new YearGroupsResource(client);

            var yearGroup = new YearGroup();
            yearGroup.Resource = resource;

            var resourceAddress = $"{YearGroupsResource.ResourceName}/{yearGroup.Code}/students";

            Mock.Get(client).Setup(c => c.GetList<Student>(resourceAddress, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(groups));

            var results = await yearGroup.Students();

            Assert.That(results.First().FirstName, Is.EqualTo(groups.First().FirstName));
        }
   }
}
