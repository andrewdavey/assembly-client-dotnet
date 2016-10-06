using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class AcademicYearsResource
    {
        public static string ResourceName => "academic_years";

        private readonly ApiClient client;

        public AcademicYearsResource(ApiClient client)
        {
            this.client = client;
        }

        public async Task<IList<AcademicYear>> All()
        {
            var results = await List(perPage: 100);
            return results;
        }

        public async Task<IList<AcademicYear>> List(int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;

            dArgs.Add("per_page", perPage);

            var results = await client.GetList<AcademicYear>(ResourceName, args);

            var configuredresults = results.Select((r) =>
            {
                r.Resource = this;
                return r;
            }).ToList();

            return configuredresults;
        }
    }
}