using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace AssemblyClient
{   
    public class Api : IApi
    {
        private HttpClient client;

        public Api(HttpClient client)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Add("Accept", "application/json; version=1");
        }

        public IList<T> get<T>(string resource, ApiConfiguration config)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.Token);

            var results = new List<T>();

            int? currentPage = 1;

            while (currentPage.HasValue) 
            {
                var url = $"{resource}?page={currentPage}";

                var response = client.GetAsync(url).Result;
                
                response.EnsureSuccessStatusCode();

                var data = response.Content.ReadAsStringAsync().Result;

                var list = JsonConvert.DeserializeObject<ApiList<T>>(data);
                
                results.AddRange(list.data);

                currentPage = list.next_page;
            }

            return results;
        }
    }
}