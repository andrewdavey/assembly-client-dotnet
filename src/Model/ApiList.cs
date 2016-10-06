using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class ApiList<T>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("current_page")]
        public int? CurrentPage { get; set; }

        [JsonProperty("next_page")]
        public int? NextPage { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        public ApiList()
        {
            this.Data = new List<T>();
        }
    }
}
