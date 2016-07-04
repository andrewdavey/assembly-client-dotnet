using System;
using System.Net.Http;
using System.Dynamic;
using System.Collections.Generic;

namespace AssemblyClient
{   
    public class TokenRefreshedEventArgs
    {
        public string Token { get; set; }
    }

    public class ApiClient
    {
        public event EventHandler<TokenRefreshedEventArgs> TokenRefreshed;

        public ApiConfiguration Configuration 
        { 
            get
            {
                return this.api.Configuration;
            } 
        }

        private readonly Api api;

        public ApiClient()
        {
            var endpoint = "https://api.assembly.education";
            
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            this.api = new Api(httpClient);
        }

        public void OnTokenRefreshed(string newToken) 
        {
            if (TokenRefreshed != null) {
                var eventArgs = new TokenRefreshedEventArgs() {
                    Token = newToken
                };

                TokenRefreshed(this, eventArgs);
            }
        }

        public virtual IList<T> GetList<T>(string resource, ExpandoObject args) 
        {
            var results = api.GetList<T>(resource, args, OnTokenRefreshed);
            return results;
        }

        public virtual T GetObject<T>(string resource, ExpandoObject args) 
        {
            var results = api.GetObject<T>(resource, args, OnTokenRefreshed);
            return results;
        }

        public ApiClient(Api api)
        {
            this.api = api;
        }

        public virtual void Configure(ApiConfiguration config)
        {
            this.api.Configuration = config;
        }

        public StudentsResource Students => new StudentsResource(this);

        public TeachingGroupsResource TeachingGroups => new TeachingGroupsResource(this);
    }
}