using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class YearGroupsResource
    {
        public const string ResourceName = "year_groups";

        private readonly ApiClient client;

        public YearGroupsResource(ApiClient client)
        {
            this.client = client;
        }

        public async Task<IList<YearGroup>> All()
        {
            var results = await List(perPage: 100);
            return results;
        }

        public async Task<IList<YearGroup>> List(string yearCode = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;
            dArgs.Add("per_page", perPage);

            var results = await client.GetList<YearGroup>(ResourceName, args);

            var configuredresults = results.Select((r) =>
            {
                r.Resource = this;
                return r;
            }).ToList();

            return configuredresults;
        }

        public async Task<YearGroup> Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = await client.GetObject<YearGroup>(resource, args);
            result.Resource = this;

            return result;
        }

        public async Task<IList<Student>> Students(string yearCode, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.per_page = perPage;

            var resource = $"{ResourceName}/{yearCode}/students";
            var results = await client.GetList<Student>(resource, args);

            return results;
        }
    }
}
