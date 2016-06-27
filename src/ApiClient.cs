using System;
using System.Collections.Generic;

namespace AssemblyClient
{   
    public class ApiClient
    {
        public ApiConfiguration Configuration { get; private set; }

        private IApi api;

        public ApiClient()
        {
            this.api = new Api("http://api.lvh.me:3000/students");
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