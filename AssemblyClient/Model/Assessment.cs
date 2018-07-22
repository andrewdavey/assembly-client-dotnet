using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Assessment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("family_id")]
        public int FamilyId { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }
    }
}