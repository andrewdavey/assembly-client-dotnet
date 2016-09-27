using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

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

        public IList<RegistrationGroup> All()
        {
            var results = List(perPage:100);
            return results;
        }

        public IList<RegistrationGroup> List(string yearCode = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;
            var dateFormat = "yyyy-MM-dd";

            dArgs.Add("year_code", yearCode);
            dArgs.Add("date", date?.ToString(dateFormat));
            dArgs.Add("start_date", startDate?.ToString(dateFormat));
            dArgs.Add("end_date", endDate?.ToString(dateFormat));  
            dArgs.Add("per_page", perPage);

            var results = client.GetList<RegistrationGroup>(ResourceName, args);

            var configuredresults = results.Select((r) => { r.Resource = this; return r; }).ToList();

            return configuredresults;
        }

        public RegistrationGroup Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = client.GetObject<RegistrationGroup>(resource, args);
            result.Resource = this;

            return result;
        }

        public IList<Student> Students(int groupId, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.per_page = perPage;

            var resource = $"{ResourceName}/{groupId}/students";
            var results = client.GetList<Student>(resource, args);

            return results;
        }
    }
}