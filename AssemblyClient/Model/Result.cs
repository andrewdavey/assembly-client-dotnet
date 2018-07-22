using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Result
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("student_id")]
        public int StudentId { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("assessment_id")]
        public int AssessmentId { get; set; }

        [JsonProperty("assessment_point_rank")]
        public int AssessmentPointRank { get; set; }

        [JsonProperty("aspect_id")]
        public int AspectId { get; set; }

        [JsonProperty("grade_id")]
        public int GradeId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
