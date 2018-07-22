using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class AcademicYear
    {
        internal AcademicYearsResource Resource { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("terms")]
        public IList<Term> Terms { get; set; }

        public AcademicYear()
        {
            this.Terms = new List<Term>();
        }
    }
}