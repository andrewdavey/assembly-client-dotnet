using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class ResultsResource : Resource
    {
        public static string ResourceName => "results";

        public ResultsResource(ApiClient client)
            : base(client)
        {
        }

        public async Task<IList<Result>> List(IEnumerable<int> studentIds, int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.perPage = perPage;
            args.students = string.Join("+", studentIds);

            var results = await Client.GetList<Result>(ResourceName, args);

            return results;
        }

        public async Task<ResultsWriteResponse> WriteResults(ResultsBatch resultsBatch)
        {
            var result = await Client.PostData<ResultsWriteResponse>(ResourceName, resultsBatch);
            return result;
        }
    }
}