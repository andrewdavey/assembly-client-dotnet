using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;

namespace AssemblyClient
{   
    public class Api : IApi
    {
        private string endpoint;
        private HttpClient client;

        public Api(string endpoint)
        {
            this.endpoint = endpoint;

            this.client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

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
                var data = response.Content.ReadAsStreamAsync().Result;
                
                var serializer = new DataContractJsonSerializer(typeof(ApiList<T>));
                ApiList<T> list = (ApiList<T>) serializer.ReadObject(data);

                results.AddRange(list.data);

                currentPage = list.next_page;
            }

            return results;
        }
    }
}