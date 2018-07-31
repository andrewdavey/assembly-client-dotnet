using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Assessment
    {
        internal AssessmentsResource Resource { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("family_id")]
        public int FamilyId { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        public async Task<GradeSet> GradeSet(int? perPage = 100)
        {
            var result = await Resource.GradeSet(Id, perPage: perPage);
            return result;
        }
    }
}