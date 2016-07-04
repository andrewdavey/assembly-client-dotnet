using System;
using System.Dynamic;
using System.Collections.Generic;
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
        public void ShouldRequestAllFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var students = new List<Student>() 
            {
                new Student(), new Student()
            };

            Mock.Get(client).Setup(c => c.GetList<Student>("students", It.IsAny<ExpandoObject>())).Returns(students);

            var studentResources = new StudentsResource(client);

            studentResources.All();

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            var students = new List<Student>() 
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
            ).Returns(students).Verifiable();

            var studentResources = new StudentsResource(client);
            var results = studentResources.List(academicYearId: academicYearId, yearCode: yearCode, demographics: demographics);
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestAStudentFromTheApi()
        {
            var client = Mock.Of<ApiClient>();

            var item = new Student();

            int studentId = 1;
            bool demographics = true;

            Mock.Get(client).Setup(c => c.GetObject<Student>($"students/{studentId}", 
                It.Is<ExpandoObject>(x => 
                x.V<bool>("demographics") == demographics))
            ).Returns(item).Verifiable();

            var studentResources = new StudentsResource(client);
            var result = studentResources.Find(studentId, demographics);

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public void ShouldRequestListFromTheApiWithOnlySomeParameters()
        {
            var client = Mock.Of<ApiClient>();

            var students = new List<Student>() 
            {
                new Student(), new Student()
            };

            bool demographics = true;

            Mock.Get(client).Setup(c => c.GetList<Student>("students", 
                It.Is<ExpandoObject>(x =>  
                x.V<bool>("demographics") == demographics &&
                x.V("year_code") == null))
            ).Returns(students).Verifiable();

            var studentResources = new StudentsResource(client);
            var results = studentResources.List(demographics: demographics);
            Assert.That(results.Count, Is.EqualTo(students.Count));

            Mock.Get(client).VerifyAll();
        }
        
    }
}