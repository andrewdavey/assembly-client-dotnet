using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace AssemblyClient
{
    public class TeachingGroupsResource
    {
        public const string ResourceName = "teaching_groups";

        private readonly ApiClient client;

        public TeachingGroupsResource(ApiClient client)
        {
            this.client = client;
        }

        public IList<TeachingGroup> All()
        {
            var results = List(perPage:100);
            return results;
        }

        public IList<TeachingGroup> List(string academicYearId = null, string subjectCode = null, string yearCode = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? perPage = null)
        {
            var args = new ExpandoObject();
            var dArgs = (IDictionary<string, object>)args;
            var dateFormat = "yyyy-MM-dd";

            dArgs.Add("academic_year_id", academicYearId);
            dArgs.Add("subject_code", subjectCode);
            dArgs.Add("year_code", yearCode);
            dArgs.Add("date", date?.ToString(dateFormat));
            dArgs.Add("start_date", startDate?.ToString(dateFormat));
            dArgs.Add("end_date", endDate?.ToString(dateFormat));  
            dArgs.Add("per_page", perPage);

            var results = client.GetList<TeachingGroup>(ResourceName, args);

            var configuredresults = results.Select((r) => { r.Resource = this; return r; }).ToList();

            return configuredresults;
        }

        public TeachingGroup Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = client.GetObject<TeachingGroup>(resource, args);
            result.Resource = this;

            return result;
        }

        public IList<Student> Students(int groupId, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.perPage = perPage;

            var resource = $"{ResourceName}/{groupId}/students";
            var results = client.GetList<Student>(resource, args);

            return results;
        }
    }
}