using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class AssessmentsResource : Resource
    {
        public static string ResourceName => "assessments";
        

        public AssessmentsResource(ApiClient client)
            : base(client)
        {
        }

        public async Task<IList<Assessment>> All()
        {
            var results = await List(perPage: 100);
            SetResourceProperty(results);
            return results;
        }

        public async Task<IList<Assessment>> List(int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.perPage = perPage;

            var results = await Client.GetList<Assessment>(ResourceName, (ExpandoObject)args);
            SetResourceProperty(results);
            return results;
        }

        private void SetResourceProperty(IList<Assessment> results)
        {
            foreach (var result in results)
            {
                result.Resource = this;
            }
        }

        public async Task<GradeSet> GradeSet(int assessmentId, int? perPage = 100)
        {
            dynamic args = new ExpandoObject();

            args.per_page = perPage;

            // https://developers.assembly.education/api/assessments/#view-grade-set-for-an-assessment
            var resource = $"{ResourceName}/{assessmentId}/grade_set";
            var result = await Client.GetObject<AssessmentWithGradeSet>(resource, (ExpandoObject)args);

            return result.GradeSet;
        }

        class AssessmentWithGradeSet
        {
            [JsonProperty("grade_set")]
            public GradeSet GradeSet { get; set; }
        }
    }
}