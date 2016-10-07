using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class StudentsResource : Resource
    {
        public const string ResourceName = "students";

        private readonly ApiClient client;

        public StudentsResource(ApiClient client)
        {
            this.client = client;
        }

        public Task<IList<Student>> All()
        {
            var results = List(perPage: 100);
            return results;
        }

        public async Task<IList<Student>> List(string academicYearId = null, string yearCode = null, bool? demographics = null, int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.academic_year_id = academicYearId;
            args.year_code = yearCode;
            args.demographics = demographics;
            args.perPage = perPage;

            var results = await client.GetList<Student>(ResourceName, args);
            return results;
        }

        public async Task<Student> Find(int studentId, bool? demographics = null)
        {
            dynamic args = new ExpandoObject();
            args.demographics = demographics;

            var resource = $"{ResourceName}/{studentId}";
            var result = await client.GetObject<Student>(resource, args);

            return result;
        }
    }
}
