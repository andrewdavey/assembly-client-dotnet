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
    public class StudentsResourceTests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            IList<Student> students = new List<Student>() 
            {
                new Student(), new Student()
            };

            Mock.Get(client).Setup(c => c.GetList<Student>("students", It.IsAny<ExpandoObject>())).Returns(Task.FromResult(students));

            var studentResources = new StudentsResource(client);

            await studentResources.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Student> students = new List<Student>() 
            {
                new Student(), new Student()
            };

            var yearCode = "a";
            var academicYearId = "123";
            bool demographics = true;

            Mock.Get(client).Setup(c => c.GetList<Student>("students", 
                It.Is<ExpandoObject>(x => 
                x.V<string>("year_code") == "a" && 
                x.V<string>("academic_year_id") == academicYearId && 
                x.V<bool>("demographics") == demographics))
            ).Returns(Task.FromResult(students)).Verifiable();

            var studentResources = new StudentsResource(client);
            var results = await studentResources.List(academicYearId: academicYearId, yearCode: yearCode, demographics: demographics);
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestAStudentFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new Student();

            int studentId = 1;
            bool demographics = true;

            Mock.Get(client).Setup(c => c.GetObject<Student>($"students/{studentId}", 
                It.Is<ExpandoObject>(x => 
                x.V<bool>("demographics") == demographics))
            ).Returns(Task.FromResult(item)).Verifiable();

            var studentResources = new StudentsResource(client);
            var result = await studentResources.Find(studentId, demographics);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithOnlySomeParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Student> students = new List<Student>() 
            {
                new Student(), new Student()
            };

            bool demographics = true;

            Mock.Get(client).Setup(c => c.GetList<Student>("students", 
                It.Is<ExpandoObject>(x =>  
                x.V<bool>("demographics") == demographics &&
                x.V("year_code") == null))
            ).Returns(Task.FromResult(students)).Verifiable();

            var studentResources = new StudentsResource(client);
            var results = await studentResources.List(demographics: demographics);
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }
        
    }
}