using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class ResultsBatch
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("assessment_id")]
        public int AssessmentId { get; set; }

        [JsonProperty("assessment_point_rank")]
        public int AssessmentPointRank { get; set; }

        [JsonProperty("aspect_id")]
        public int AspectId { get; set; }

        [JsonProperty("results")]
        public IList<ResultsBatchResult> Results { get; }

        public ResultsBatch()
        {
            this.Results = new List<ResultsBatchResult>();
        }

        public void AddResult(int studentId, int gradeId)
        {
            this.Results.Add(new ResultsBatchResult { StudentId = studentId, GradeId = gradeId });
        }
    }
}
