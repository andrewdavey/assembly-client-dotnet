using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Portable.Text;

namespace AssemblyClient
{
    public class Api
    {
        private readonly HttpClient client;

        public ApiConfiguration Configuration { get; set; }

        public Api()
        {

        }

        public Api(HttpClient client)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Add("Accept", "application/json; version=1");
        }

        public virtual string load(string resource, Action<string> tokenRefreshed)
        {
            var result = load(resource, new ExpandoObject(), tokenRefreshed);
            return result;
        }

        private string RefreshToken(string refreshToken)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Configuration.BasicAuth);

            var refreshRequest = new ApiGrant(refreshToken);
            var refreshData = JsonConvert.SerializeObject(refreshRequest);
            var body = new StringContent(refreshData, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/oauth/token", body).Result;

            response.EnsureSuccessStatusCode();

            var refreshedToken = response.Deserialize();
            return refreshedToken.access_token;
        }
        
        public virtual string load(string resource, ExpandoObject args, Action<string> tokenRefreshed)
        {
            var query = args.ToParams();
            var resourceWithQuery = $"{resource}";

            if (!string.IsNullOrEmpty(query))
            {
                resourceWithQuery = $"{resourceWithQuery}?{query}";
            }

            var response = client.MakeRequest(resourceWithQuery, Configuration.Token).Result;
            var isTokenValid = response.IsValidToken();

            if (!isTokenValid)
            {
                var newToken = RefreshToken(Configuration.RefreshToken);
                Configuration.Token = newToken;
                tokenRefreshed(newToken);

                response = client.MakeRequest(resourceWithQuery, Configuration.Token).Result;
                response.EnsureSuccessStatusCode();
            }
            else
            {
                response.EnsureSuccessStatusCode();    
            }

            var result = response.Content.ReadAsStringAsync().Result;
            return result; 
        }

        private ExpandoObject FormatData(IDictionary<string, object> me)
        {
            var target = (IDictionary<string, object>)new ExpandoObject();

            foreach(var v in me)
            {
                if (v.Key == "object") continue;
                target.Add(v.Key.ToProperty(), v.Value);
            }

            return (ExpandoObject)target;
        }

        public virtual IList<T> GetList<T>(string resource, ExpandoObject args, Action<string> tokenRefreshed)
        {
            var results = new List<T>();

            dynamic pagedArgs = args;

            int? currentPage = 1;

            while (currentPage.HasValue) 
            {
                pagedArgs.page = currentPage;

                var data = load(resource, pagedArgs, tokenRefreshed);

                var list = JsonConvert.DeserializeObject<ApiList<T>>(data);

                results.AddRange(list.data);

                currentPage = list.next_page;
            }

            return results;
        }

        public virtual T GetObject<T>(string resource, ExpandoObject args, Action<string> tokenRefreshed)
        {
            var data = load(resource, args, tokenRefreshed);
            var obj = JsonConvert.DeserializeObject<T>(data);
            return obj;
        } 
    }
}