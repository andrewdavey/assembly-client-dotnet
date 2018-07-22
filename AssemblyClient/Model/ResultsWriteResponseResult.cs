using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class ResultsWriteResponseResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("student_id")]
        public int StudentId { get; set; }

        [JsonProperty("grade_id")]
        public int GradeId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
