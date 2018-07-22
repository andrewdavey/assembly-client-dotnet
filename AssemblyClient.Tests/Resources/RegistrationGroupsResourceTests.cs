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
    public class RegistrationGroupsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<RegistrationGroup> groups = new List<RegistrationGroup>()
            {
                new RegistrationGroup(), new RegistrationGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<RegistrationGroup>(RegistrationGroupsResource.ResourceName, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(groups));

            var resource = new RegistrationGroupsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<RegistrationGroup> registrationGroups = new List<RegistrationGroup>()
            {
                new RegistrationGroup(), new RegistrationGroup()
            };

            var yearCode = "a";

            Mock.Get(client).Setup(c => c.GetList<RegistrationGroup>(
                RegistrationGroupsResource.ResourceName,
                It.Is<ExpandoObject>(x =>
                x.V<string>("year_code") == "a"))).Returns(Task.FromResult(registrationGroups)).Verifiable();

            var resource = new RegistrationGroupsResource(client);
            var results = await resource.List(yearCode: yearCode);
            Assert.That(results.Count, Is.EqualTo(registrationGroups.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestAnItemFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new RegistrationGroup();

            int itemId = 1;

            Mock.Get(client).Setup(c => c.GetObject<RegistrationGroup>(
                $"registration_groups/{itemId}",
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(item)).Verifiable();

            var registrationGroupResource = new RegistrationGroupsResource(client);
            var result = await registrationGroupResource.Find(itemId);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithOnlySomeParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<RegistrationGroup> groups = new List<RegistrationGroup>()
            {
                new RegistrationGroup(), new RegistrationGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<RegistrationGroup>(
                "registration_groups",
                It.Is<ExpandoObject>(x => x.V("year_code") == null))).Returns(Task.FromResult(groups)).Verifiable();

            var registrationGroupResource = new RegistrationGroupsResource(client);
            var results = await registrationGroupResource.List();
            Assert.That(results.Count, Is.EqualTo(groups.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShoudFetchARegistrationGroup()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://test.lvh.me/registration-group/1")
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

            var result = await api.GetObject<RegistrationGroup>("registration-group/1", emptyArgs);

            Assert.That(result.Name, Is.EqualTo("NAME"));
        }

        [Test]
        public async Task RegistrationGroupFetchesStudents()
        {
            IList<Student> groups = new List<Student>()
            {
                new Student(), new Student()
            };

            var client = Mock.Of<ApiClient>();

            var resource = new RegistrationGroupsResource(client);

            var registrationGroup = new RegistrationGroup();
            registrationGroup.Resource = resource;

            var resourceAddress = $"{RegistrationGroupsResource.ResourceName}/{registrationGroup.Id}/students";

            Mock.Get(client).Setup(c => c.GetList<Student>(resourceAddress, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(groups));

            var results = await registrationGroup.Students();

            Assert.That(results.First().FirstName, Is.EqualTo(groups.First().FirstName));
        }
   }
}
