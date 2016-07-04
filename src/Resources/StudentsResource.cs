using System.Collections.Generic;
using System.Dynamic;

namespace AssemblyClient
{
    public class StudentsResource
    {
        public const string ResourceName = "students";

        private readonly ApiClient client;

        public StudentsResource(ApiClient client)
        {
            this.client = client;
        }

        public IList<Student> All()
        {
            var results = List(perPage:100);
            return results;
        }

        public IList<Student> List(string academicYearId = null, string yearCode = null, bool? demographics = null, int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.academic_year_id = academicYearId;
            args.year_code = yearCode;
            args.demographics = demographics;
            args.perPage = perPage;

            var results = client.GetList<Student>(ResourceName, args);
            return results;
        }

        public Student Find(int studentId, bool? demographics = null)
        {
            dynamic args = new ExpandoObject();
            args.demographics = demographics;

            var resource = $"{ResourceName}/{studentId}";
            var result = client.GetObject<Student>(resource, args);

            return result;
        }
    }
}