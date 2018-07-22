using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class ResultsWriteResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IList<ResultsWriteResponseResult> Results { get; }

        public ResultsWriteResponse()
        {
            this.Results = new List<ResultsWriteResponseResult>();
        }
    }
}
