using System;
using System.Net.Http;
using System.Collections.Generic;

namespace AssemblyClient
{   
    public class ApiClient
    {
        public ApiConfiguration Configuration { get; private set; }

        private IApi api;

        public ApiClient()
        {
            var endpoint = "https://api.assembly.education";
            
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            this.api = new Api(httpClient);
        }

        public ApiClient(IApi api)
        {
            this.api = api;
        }

        public void Configure(ApiConfiguration config)
        {
            this.Configuration = config;
        }

        public IList<Student> Students()
        {
            var results = api.get<Student>("students", Configuration);
            return results;
        }
    }
}