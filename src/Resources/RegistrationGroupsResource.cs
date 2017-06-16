using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class RegistrationGroupsResource : Resource
    {
        public static string ResourceName => "registration_groups";

        public RegistrationGroupsResource(ApiClient client)
            : base(client)
        {
        }

        public async Task<IList<RegistrationGroup>> All()
        {
            var results = await List(perPage: 100);
            return results;
        }

        public async Task<IList<RegistrationGroup>> List(string yearCode = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;

            dArgs.Add("year_code", yearCode);
            dArgs.Add("date", date?.ToString(DateFormat));
            dArgs.Add("start_date", startDate?.ToString(DateFormat));
            dArgs.Add("end_date", endDate?.ToString(DateFormat));
            dArgs.Add("per_page", perPage);

            var results = await Client.GetList<RegistrationGroup>(ResourceName, args);

            var configuredresults = results.Select((r) =>
            {
                r.Resource = this;
                return r;
            }).ToList();

            return configuredresults;
        }

        public async Task<RegistrationGroup> Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = await Client.GetObject<RegistrationGroup>(resource, args);
            result.Resource = this;

            return result;
        }

        public async Task<IList<Student>> Students(int groupId, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.per_page = perPage;

            var resource = $"{ResourceName}/{groupId}/students";
            var results = await Client.GetList<Student>(resource, args);

            return results;
        }
    }
}
