using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class GradeSet
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("grades")]
        public IList<Grade> Grades { get; set; }

        public GradeSet()
        {
            this.Grades = new List<Grade>();
        }
    }
}