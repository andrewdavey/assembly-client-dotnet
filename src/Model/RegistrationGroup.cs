using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace AssemblyClient
{   
    public class RegistrationGroup
    {
        internal RegistrationGroupsResource Resource { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("supervisor_ids")]
        public IList<int> SupervisorIds { get; set; }

        [JsonProperty("student_ids")]
        public IList<int> StudentIds { get; set; }

        public async Task<IList<Student>> Students(int? perPage = 100)
        {
            var results = await Resource.Students(Id, perPage: perPage);
            return results;
        }
    }
}