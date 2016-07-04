using System;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using AssemblyClient;
using System.Net.Http;
using RichardSzalay.MockHttp;
using System.Net;

namespace AssemblyClientTests
{

    [TestFixture]
    public class TeachingGroupResourceTests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var students = new List<TeachingGroup>() 
            {
                new TeachingGroup(), new TeachingGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>("teaching_groups", It.IsAny<ExpandoObject>())).Returns(students);

            var resource = new TeachingGroupsResource(client);

            resource.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            var teachingGroups = new List<TeachingGroup>() 
            {
                new TeachingGroup(), new TeachingGroup()
            };

            var yearCode = "a";
            var academicYearId = "123";
            
            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>("teaching_groups", 
                It.Is<ExpandoObject>(x => 
                x.V<string>("year_code") == "a" && 
                x.V<string>("academic_year_id") == academicYearId))
            ).Returns(teachingGroups).Verifiable();

            var resource = new TeachingGroupsResource(client);
            var results = resource.List(academicYearId: academicYearId, yearCode: yearCode);
            Assert.That(results.Count, Is.EqualTo(teachingGroups.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestAnItemFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new TeachingGroup();

            int itemId = 1;

            Mock.Get(client).Setup(c => c.GetObject<TeachingGroup>($"teaching_groups/{itemId}", It.IsAny<ExpandoObject>())
            ).Returns(item).Verifiable();

            var teachingGroupsResource = new TeachingGroupsResource(client);
            var result = teachingGroupsResource.Find(itemId);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestListFromTheApiWithOnlySomeParameters()
        {
            var client = Mock.Of<ApiClient>();

            var students = new List<TeachingGroup>() 
            {
                new TeachingGroup(), new TeachingGroup()
            };

            Mock.Get(client).Setup(c => c.GetList<TeachingGroup>("teaching_groups", 
                It.Is<ExpandoObject>(x =>  x.V("year_code") == null))
            ).Returns(students).Verifiable();

            var teachingGroupsResource = new TeachingGroupsResource(client);
            var results = teachingGroupsResource.List();
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShoudFetchATeachingGroup()
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

            var result = api.GetObject<TeachingGroup>("teaching-group/1", emptyArgs);

            Assert.That(result.Subject.Code, Is.EqualTo("MA"));
        }

        [Test]
        public void TeachingGroupFetchesStudents()
        {
            var students = new List<Student>() 
            {
                new Student(), new Student()
            };
            
            var client = Mock.Of<ApiClient>();
           
            var resource = new TeachingGroupsResource(client);

            var teachingGroup = new TeachingGroup();
            teachingGroup.Resource = resource;

            var resourceAddress = $"{TeachingGroupsResource.ResourceName}/{teachingGroup.Id}/students";

            Mock.Get(client).Setup(c => c.GetList<Student>(resourceAddress, It.IsAny<ExpandoObject>())).Returns(students);

            var results = teachingGroup.Students();

            Assert.That(results.First().FirstName, Is.EqualTo(students.First().FirstName));
        }
    }
}