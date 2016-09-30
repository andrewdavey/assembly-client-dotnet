using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class ApiClientTests
    {
        ApiClient client;

        ApiConfiguration config;
        Api api;
        IList<Student> students;
        Student student;

        string token;
        string refreshToken;
        string clientId;
        string clientSecret;
        string resource;

        [SetUp]
        public void Setup()
        {
            token = new Random().Next().ToString();
            refreshToken = new Random().Next().ToString();
            clientId = new Random().Next().ToString();
            clientSecret = new Random().Next().ToString();

            resource = new Random().Next().ToString();

            api = Mock.Of<Api>();

            config = new ApiConfiguration
            {
                Token = token,
                RefreshToken = refreshToken,
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            students = new List<Student>() 
            {
                new Student(), new Student()
            };

            student = new Student();
            
            client = new ApiClient(api);
            client.Configure(config);
        }

        [TearDown]
        public void TearDown()
        {
            Mock.Get(api).VerifyAll();
        }

        [Test]
        public void AcceptsTheConfiguration()
        {
            Assert.That(client.Configuration.Token, Is.EqualTo(token));
            Assert.That(client.Configuration.RefreshToken, Is.EqualTo(refreshToken));
            Assert.That(client.Configuration.ClientId, Is.EqualTo(clientId));
            Assert.That(client.Configuration.ClientSecret, Is.EqualTo(clientSecret));
        }

        [Test]
        public async Task ShouldGetAListFromTheApi()
        {
            Mock.Get(api)
                .Setup(x => x.GetList<Student>(resource, It.IsAny<ExpandoObject>()))
                .Returns(Task.FromResult(students))
                .Verifiable();

            await client.GetList<Student>(resource, new ExpandoObject());
        }

        [Test]
        public async Task ShouldGetAnObjectFromTheApi()
        {
            Mock.Get(api)
                .Setup(x => x.GetObject<Student>(resource, It.IsAny<ExpandoObject>()))
                .Returns(Task.FromResult(student))
                .Verifiable();

            var result = await client.GetObject<Student>(resource, new ExpandoObject());
            Assert.That(result, Is.EqualTo(student));
        }
    }
}