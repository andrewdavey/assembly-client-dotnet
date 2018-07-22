using System.Dynamic;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using AssemblyClient;

namespace AssemblyClientTests
{
    [TestFixture]
    public class ResultsResourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldRequestListFromTheApiWithAllParameters()
        {
            var client = Mock.Of<ApiClient>();

            IList<Result> results = new List<Result>()
            {
                new Result(), new Result()
            };

            Mock.Get(client).Setup(c => c.GetList<Result>(
                ResultsResource.ResourceName,
                It.IsAny<ExpandoObject>())).Returns(Task.FromResult(results)).Verifiable();

            var resource = new ResultsResource(client);
            var apiResults = await resource.List(new List<int> { 1 });
            Assert.That(apiResults.Count, Is.EqualTo(results.Count));

            Mock.Get(client).VerifyAll();
        }

        [Test]
        public async Task ShouldWriteResultsToTheApi()
        {
            var client = Mock.Of<ApiClient>();
            var subjectId = 0;

            var resultsBatch = new ResultsBatch
            {
                SubjectId = 1
            };

            var expectedResponse = new ResultsWriteResponse();

            Mock.Get(client)
                .Setup(c => c.PostData<ResultsWriteResponse>(ResultsResource.ResourceName, It.IsAny<ResultsBatch>()))
                .Callback((string res, object obj) => subjectId = ((ResultsBatch)obj).SubjectId)
                .Returns(Task.FromResult(expectedResponse));

            var resource = new ResultsResource(client);
            var response = await resource.WriteResults(resultsBatch);
            Assert.That(subjectId, Is.EqualTo(1));
            Assert.That(response, Is.SameAs(expectedResponse));

            Mock.Get(client).VerifyAll();
        }
    }
}