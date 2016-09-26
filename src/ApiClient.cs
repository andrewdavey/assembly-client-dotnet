using System;
using System.Net.Http;
using System.Dynamic;
using System.Collections.Generic;

namespace AssemblyClient
{   

    public enum AssemblyEnvironment 
    {
        Production,
        Sandbox
    }

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

        public ApiClient() : this(AssemblyEnvironment.Production) {}

        public ApiClient(AssemblyEnvironment environment)
        {
            var endpoint = "";

            switch(environment) 
            {
                case AssemblyEnvironment.Sandbox:
                    endpoint = "https://api-sandbox.assembly.education";
                    break;
                default:
                    endpoint = "https://api.assembly.education";
                    break;       
            }
            
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            this.api = new Api(httpClient);
        }

        public ApiClient(Api api)
        {
            this.api = api;
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

        public virtual void Configure(ApiConfiguration config)
        {
            this.api.Configuration = config;
        }

        public StudentsResource Students => new StudentsResource(this);

        public TeachingGroupsResource TeachingGroups => new TeachingGroupsResource(this);
    }
}