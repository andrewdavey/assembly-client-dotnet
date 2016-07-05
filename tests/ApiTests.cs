using System.Linq;
using System.Net.Http;
using System.Dynamic;
using System.Net;
using NUnit.Framework;
using AssemblyClient;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;

namespace AssemblyClientTests
{
    [TestFixture]
    public class ApiTests
    {
        HttpClient client;
        string refreshedToken;
        Action<string> refreshHandler;

        ApiConfiguration config;
            

        ExpandoObject emptyArgs;

        [SetUp]
        public void Setup()
        {
            var mockHttp = new MockHttpMessageHandler();

            refreshHandler = (token) => { };

            emptyArgs = new ExpandoObject();

            config = new ApiConfiguration();
            config.Token = new Random().Next().ToString();

            refreshedToken = new Random().Next(1000).ToString();

            mockHttp.When("http://test.lvh.me/students").Respond(HttpStatusCode.NotFound);
            mockHttp.When(HttpMethod.Post, "http://test.lvh.me/oauth/token").Respond("application/json", $"{{'access_token' : '{refreshedToken}'}}");
            
            mockHttp.When("http://test.lvh.me/correct-accept").WithHeaders(new Dictionary<string, string>() 
            { 
                { "Accept", "application/vnd.assembly+json; version=1"},
            })
            .Respond(HttpStatusCode.OK, "application/json", "{'data' : {}}");

            mockHttp.When("http://test.lvh.me/need-auth").WithHeaders(new Dictionary<string, string>() 
                { 
                    { "Authorization", $"Bearer {config.Token}" }
                })
                .Respond(HttpStatusCode.Unauthorized, "application/json", "{'error' : 'invalid_token'}");
            
            mockHttp.When("http://test.lvh.me/need-auth").WithHeaders(new Dictionary<string, string>() 
                { 
                    {"Authorization", $"Bearer {refreshedToken}"}
                })
                .Respond(HttpStatusCode.OK, "application/json", "{'first_name' : 'Nick'}");

            mockHttp.When("http://test.lvh.me/with-params").WithQueryString("a=1&b=2").Respond(HttpStatusCode.OK, "application/json", "{}");
            mockHttp.When("http://test.lvh.me/single").Respond(HttpStatusCode.OK, "application/json", "{'first_name':'Andy', 'type':'object'}");
            mockHttp.When("http://test.lvh.me/many-pages?page=1").Respond(HttpStatusCode.OK, "application/json", "{'current_page' : 1, 'next_page': 2, 'data': []}");
            mockHttp.When("http://test.lvh.me/many-pages?page=2").Respond(HttpStatusCode.OK, "application/json", "{'current_page' : 2, 'next_page': null, 'data': []}");
            mockHttp.When("http://test.lvh.me/properties?page=1").Respond(HttpStatusCode.OK, "application/json", "{'current_page' : 1, 'next_page': null, 'data': [{'type':'student', 'first_name':'Nick'}]}");
            mockHttp.When("http://test.lvh.me/teaching-group/1").Respond(HttpStatusCode.OK, "application/json", "{ 'object': 'teaching_group', 'id': 1, 'name': '7x/Ma1', 'academic_year_id': 22, 'supervisor_ids': [ 1 ], 'student_ids': [ 15, 50, 109 ], 'subject': { 'object': 'subject', 'id': 2, 'code': 'MA', 'name': 'Mathematics' } } ");
             
            client = new HttpClient(mockHttp)
            {
                BaseAddress = new System.Uri("http://test.lvh.me")
            };
        }

        [Test]
        public void ShouldThrowAnExceptionIfTheResponseIsNotOk()
        {
            var api = new Api(client);
            api.Configuration = config;

            Assert.Throws<HttpRequestException>(() => api.GetList<Student>("students", emptyArgs));
        }

        [Test]
        public void ShouldHaveTheCorrectAcceptHeader()
        {
            var api = new Api(client);
            api.Configuration = config;

            api.load("correct-accept", emptyArgs);
        }

        [Test]
        public void ShouldRefreshTheTokenIfUnauthorized()
        {
            var api = new Api(client);
            api.Configuration = config;

            api.TokenRefreshed += (sender, args) => {
                Assert.That(args.Token, Is.EqualTo(refreshedToken));
            };

            api.load("need-auth", emptyArgs);
        }

        [Test]
        public void ShouldGetWithTheSuppliedParameters()
        {
            var api = new Api(client);
            api.Configuration = config;

            dynamic args = new ExpandoObject();
            args.a = 1;
            args.b = "2";

            api.load("with-params", args);
        }

        [Test]
        public void ShouldGetAllTheSuppliedPages()
        {
            var api = new Api(client);
            api.Configuration = config;

            api.GetList<Student>("many-pages", emptyArgs);
        }

        [Test]
        public void ShouldReturnCorrectPropertyStyle()
        {
            var api = new Api(client);
            api.Configuration = config;

            var results = api.GetList<Student>("properties", emptyArgs);

            Assert.That(results.Count, Is.EqualTo(1));

            Assert.That(results.First().FirstName, Is.EqualTo("Nick"));
        }

        [Test]
        public void ShouldFetchAnObject()
        {
            var api = new Api(client);
            api.Configuration = config;

            var result = api.GetObject<Student>("single", emptyArgs);

            Assert.That(result.FirstName, Is.EqualTo("Andy"));
        }

        [Test]
        public void ShouldUpdateTheTokenIfARefreshOccurs()
        {
            var api = new Api(client);
            api.Configuration = config;

            var result = api.load("/need-auth");
            
            Assert.That(api.Configuration.Token, Is.EqualTo(refreshedToken));
        }
    }
}