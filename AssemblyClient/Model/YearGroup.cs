using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class YearGroup
    {
        internal YearGroupsResource Resource { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("supervisor_ids")]
        public IList<int> SupervisorIds { get; set; }

        [JsonProperty("student_ids")]
        public IList<int> StudentIds { get; set; }

        public async Task<IList<Student>> Students(int? perPage = 100)
        {
            var results = await Resource.Students(Code, perPage: perPage);
            return results;
        }
    }
}