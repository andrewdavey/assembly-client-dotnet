using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Aspect
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}