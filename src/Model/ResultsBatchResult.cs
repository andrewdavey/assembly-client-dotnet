using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class ResultsBatchResult
    {
        [JsonProperty("student_id")]
        public int StudentId { get; set; }

        [JsonProperty("grade_id")]
        public int GradeId { get; set; }
    }
}
