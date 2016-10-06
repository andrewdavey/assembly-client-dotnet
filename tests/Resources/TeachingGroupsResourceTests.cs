using System;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using Moq;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class TeachingGroupsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<TeachingGroup> students = new List<TeachingGroup>()
            {
                new TeachingGroup(), new TeachingGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>("teaching_groups", It.IsAny<ExpandoObject>())).Returns(Task.FromResult(students));

            var resource = new TeachingGroupsResource(client);

            await resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<TeachingGroup> teachingGroups = new List<TeachingGroup>()
            {
                new TeachingGroup(), new TeachingGroup()
            };

            var yearCode = "a";
            var subjectCode = "123";

            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>(
                "teaching_groups",
                It.Is<ExpandoObject>(x =>
                x.V<string>("year_code") == "a" &&
                x.V<string>("subject_code") == subjectCode))).Returns(Task.FromResult(teachingGroups)).Verifiable();

            var resource = new TeachingGroupsResource(client);
            var results = await resource.List(subjectCode: subjectCode, yearCode: yearCode);
            Assert.That(results.Count, Is.EqualTo(teachingGroups.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestAnItemFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new TeachingGroup();

            int itemId = 1;

            Mock.Get(client).Setup(c => c.GetObject<TeachingGroup>(
                $"teaching_groups/{itemId}", It.IsAny<ExpandoObject>())).Returns(Task.FromResult(item)).Verifiable();

            var teachingGroupsResource = new TeachingGroupsResource(client);
            var result = await teachingGroupsResource.Find(itemId);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithOnlySomeParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<TeachingGroup> students = new List<TeachingGroup>()
            {
                new TeachingGroup(), new TeachingGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>(
                "teaching_groups",
                It.Is<ExpandoObject>(x => x.V("year_code") == null))).Returns(Task.FromResult(students)).Verifiable();

            var teachingGroupsResource = new TeachingGroupsResource(client);
            var results = await teachingGroupsResource.List();
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShoudFetchATeachingGroup()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://test.lvh.me/teaching-group/1")
                .Respond(HttpStatusCode.OK, "application/json", "{ 'object': 'teaching_group', 'id': 1, 'name': '7x/Ma1', 'academic_year_id': 22, 'supervisor_ids': [ 1 ], 'student_ids': [ 15, 50, 109 ], 'subject': { 'object': 'subject', 'id': 2, 'code': 'MA', 'name': 'Mathematics' } } ");

            var client = new HttpClient(mockHttp)
            {
                BaseAddress = new System.Uri("http://test.lvh.me")
            };

            var api = new Api(client);
            var config = new ApiConfiguration();
            api.Configuration = config;

            var emptyArgs = new ExpandoObject();
            Action<string> refreshHandler = (token) => { };

            var result = await api.GetObject<TeachingGroup>("teaching-group/1", emptyArgs);

            Assert.That(result.Subject.Code, Is.EqualTo("MA"));
        }

        [Test]
        public async Task TeachingGroupFetchesStudents()
        {
            IList<Student> students = new List<Student>()
            {
                new Student(), new Student()
            };

            var client = Mock.Of<ApiClient>();

            var resource = new TeachingGroupsResource(client);

            var teachingGroup = new TeachingGroup();
            teachingGroup.Resource = resource;

            var resourceAddress = $"{TeachingGroupsResource.ResourceName}/{teachingGroup.Id}/students";

            Mock.Get(client).Setup(c => c.GetList<Student>(resourceAddress, It.IsAny<ExpandoObject>())).Returns(Task.FromResult(students));

            var results = await teachingGroup.Students();

            Assert.That(results.First().FirstName, Is.EqualTo(students.First().FirstName));
        }
   }
}
