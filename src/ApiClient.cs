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
        public event EventHandler<TokenRefreshedEventArgs> TokenRefreshed 
        {
            add
            {
                lock (api.TokenRefreshed)
                {
                    api.TokenRefreshed += value;
                }
            }
            remove
            {
                lock (api.TokenRefreshed)
                {
                    api.TokenRefreshed -= value;
                }
            }
        }

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

        public virtual IList<T> GetList<T>(string resource, ExpandoObject args)
        {
            var results = api.GetList<T>(resource, args);
            return results;
        }

        public virtual T GetObject<T>(string resource, ExpandoObject args)
        {
            var results = api.GetObject<T>(resource, args);
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