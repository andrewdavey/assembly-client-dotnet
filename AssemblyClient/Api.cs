using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public class Api
    {
        private readonly HttpClient client;

        public ApiConfiguration Configuration { get; set; }

        public EventHandler<TokenRefreshedEventArgs> TokenRefreshed { get; set; }

        internal void OnTokenRefreshed(string newToken)
        {
            if (TokenRefreshed != null)
            {
                TokenRefreshed(this, new TokenRefreshedEventArgs { Token = newToken });
            }
        }

        public Api()
        {
        }

        public Api(HttpClient client)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Add("Accept", "application/vnd.assembly+json; version=1");
        }

        public virtual async Task<T> PostData<T>(string uri, object data)
        {
            var response = await client.PostData(uri, Configuration.Token, data);
            var isTokenValid = response.IsValidToken();

            if (!isTokenValid)
            {
                var newToken = RefreshToken(Configuration.RefreshToken);
                Configuration.Token = newToken;

                OnTokenRefreshed(newToken);

                response = await client.PostData(uri, Configuration.Token, data);
                response.EnsurePlatformSuccess();
            }
            else
            {
                response.EnsurePlatformSuccess();
            }

            var result = response.Deserialize<T>();
            return result;
        }

        public virtual async Task<string> Load(string resource)
        {
            var result = await Load(resource, new ExpandoObject());
            return result;
        }

        private string RefreshToken(string refreshToken)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Configuration.BasicAuth);

            var refreshRequest = new ApiGrant(refreshToken);
            var response = client.PostData("/oauth/token", Configuration.Token, refreshRequest).Result;

            response.EnsureSuccessStatusCode();

            var refreshedToken = response.Deserialize();
            return refreshedToken.access_token;
        }

        public virtual async Task<string> Load(string resource, ExpandoObject args)
        {
            var query = args.ToParams();
            var resourceWithQuery = $"{resource}";

            if (!string.IsNullOrEmpty(query))
            {
                resourceWithQuery = $"{resourceWithQuery}?{query}";
            }

            var response = await client.MakeRequest(resourceWithQuery, Configuration.Token);
            var isTokenValid = response.IsValidToken();

            if (!isTokenValid)
            {
                var newToken = RefreshToken(Configuration.RefreshToken);
                Configuration.Token = newToken;

                OnTokenRefreshed(newToken);

                response = await client.MakeRequest(resourceWithQuery, Configuration.Token);
                response.EnsurePlatformSuccess();
            }
            else
            {
                response.EnsurePlatformSuccess();
            }

            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        private ExpandoObject FormatData(IDictionary<string, object> me)
        {
            var target = (IDictionary<string, object>)new ExpandoObject();

            foreach (var v in me)
            {
                if (v.Key == "object")
                {
                    continue;
                }

                target.Add(v.Key.ToProperty(), v.Value);
            }

            return (ExpandoObject)target;
        }

        public virtual async Task<IList<T>> GetList<T>(string resource, ExpandoObject args)
        {
            var results = new List<T>();

            dynamic pagedArgs = args;

            int? currentPage = 1;

            while (currentPage.HasValue)
            {
                pagedArgs.page = currentPage;

                var data = await Load(resource, pagedArgs);

                var list = JsonConvert.DeserializeObject<ApiList<T>>(data);

                results.AddRange(list.Data);

                currentPage = list.NextPage;
            }

            return results;
        }

        public virtual async Task<T> GetObject<T>(string resource, ExpandoObject args)
        {
            var data = await Load(resource, args);
            var obj = JsonConvert.DeserializeObject<T>(data);
            return obj;
        }
    }
}
