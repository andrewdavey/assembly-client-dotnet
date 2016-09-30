using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class RegistrationGroupsResource
    {
        public const string ResourceName = "registration_groups";

        private readonly ApiClient client;

        public RegistrationGroupsResource(ApiClient client)
        {
            this.client = client;
        }

        public async Task<IList<RegistrationGroup>> All()
        {
            var results = await List(perPage:100);
            return results;
        }

        public async Task<IList<RegistrationGroup>> List(string yearCode = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;
            var dateFormat = "yyyy-MM-dd";

            dArgs.Add("year_code", yearCode);
            dArgs.Add("date", date?.ToString(dateFormat));
            dArgs.Add("start_date", startDate?.ToString(dateFormat));
            dArgs.Add("end_date", endDate?.ToString(dateFormat));  
            dArgs.Add("per_page", perPage);

            var results = await client.GetList<RegistrationGroup>(ResourceName, args);

            var configuredresults = results.Select((r) => { r.Resource = this; return r; }).ToList();

            return configuredresults;
        }

        public async Task<RegistrationGroup> Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = await client.GetObject<RegistrationGroup>(resource, args);
            result.Resource = this;

            return result;
        }

        public async Task<IList<Student>> Students(int groupId, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.per_page = perPage;

            var resource = $"{ResourceName}/{groupId}/students";
            var results = await client.GetList<Student>(resource, args);

            return results;
        }
    }
}