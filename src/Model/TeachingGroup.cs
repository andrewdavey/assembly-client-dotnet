using Newtonsoft.Json;
using System.Collections.Generic;

namespace AssemblyClient
{   
    public class TeachingGroup
    {
        internal TeachingGroupsResource Resource { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("academic_year_id")]
        public int AcademicYearId { get; set; }

        [JsonProperty("supervisor_ids")]
        public IList<int> SupervisorIds { get; set; }

        [JsonProperty("student_ids")]
        public IList<int> StudentIds { get; set; }

        [JsonProperty("subject")]
        public Subject Subject { get; set; }

        public IList<Student> Students(int? perPage = 100)
        {
            var results = Resource.Students(Id, perPage: perPage);
            return results;
        }
    }
}