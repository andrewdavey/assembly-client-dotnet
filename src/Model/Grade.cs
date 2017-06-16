using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Grade
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}