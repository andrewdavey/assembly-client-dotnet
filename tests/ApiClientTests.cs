using System;
using NUnit.Framework;
using AssemblyClient;

namespace AllTheTests
{
    [TestFixture]
    public class SomeTests
    {
        ApiClient client;
        IApi api;

        string token;
        string refreshToken;
        string clientId;
        string clientSecret;

        [SetUp]
        public void Setup()
        {
            token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYmYiOjE0NjcwMzQwNzUsImlzcyI6Imh0dHBzOi8vcGxhdGZvcm0uYXNzZW1ibHkuZWR1Y2F0aW9uIiwiaWF0IjoxNDY3MDM0MDc1LCJsZXZlbCI6InNjaG9vbCIsInNjb3BlcyI6WyJyZWdpc3RyYXRpb25fZ3JvdXBzIiwic2Nob29sIiwic3RhZmZfbWVtYmVycyIsInN0dWRlbnRfZGVtb2dyYXBoaWNzIiwic3R1ZGVudHMiLCJ0ZWFjaGluZ19ncm91cHMiLCJ5ZWFyX2dyb3VwcyJdLCJhcHBfaWQiOjEsInNjaG9vbF9pZCI6MSwiZXhwIjoxNDY5NjI2MDc1fQ.uoDEd7tAFHRMeNYBG-RtAvZJY0jPKEmLuLFa5PrYtJc";//new Random().Next().ToString();
            refreshToken = new Random().Next().ToString();
            clientId = new Random().Next().ToString();
            clientSecret = new Random().Next().ToString();

            var config = new ApiConfiguration
            {
                Token = token,
                RefreshToken = refreshToken,
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            api = new MockApi();

            client = new ApiClient(api);
            client.Configure(config);
        }

        [Test]
        public void AcceptsTheConfiguration()
        {
            Assert.That(client.Configuration.Token, Is.EqualTo(token));
            Assert.That(client.Configuration.RefreshToken, Is.EqualTo(refreshToken));
            Assert.That(client.Configuration.ClientId, Is.EqualTo(clientId));
            Assert.That(client.Configuration.ClientSecret, Is.EqualTo(clientSecret));
        }

        [TestCase(1)]
        public void RequestsAListOfStudentsAGivenSchool(int schoolId)
        {
            var students = client.Students();
            Assert.That(students.Count, Is.EqualTo(2));
        }
    }
}