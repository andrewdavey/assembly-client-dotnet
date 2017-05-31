using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class SubjectsResource : Resource
    {
        public static string ResourceName => "subjects";

        private readonly ApiClient client;

        public SubjectsResource(ApiClient client)
        {
            this.client = client;
        }

        public async Task<IList<Subject>> All()
        {
            var results = await List(perPage: 100);
            return results;
        }

        public async Task<IList<Subject>> List(int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.perPage = perPage;

            var results = await client.GetList<Subject>(ResourceName, args);

            return results;
        }
    }
}