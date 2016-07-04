using System.Collections.Generic;
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

        public IList<TeachingGroup> List(string academicYearId = null, string subjectCode = null, string yearCode = null, int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.academic_year_id = academicYearId;
            args.subject_code = subjectCode;
            args.year_code = yearCode;
            args.perPage = perPage;

            var results = client.GetList<TeachingGroup>(ResourceName, args);
            return results;
        }

        public TeachingGroup Find(int groupId)
        {
            dynamic args = new ExpandoObject();

            var resource = $"{ResourceName}/{groupId}";
            var result = client.GetObject<TeachingGroup>(resource, args);

            return result;
        }
    }
}