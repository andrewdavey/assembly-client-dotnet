namespace AssemblyClient
{   
    public class ApiClient
    {
        public ApiConfiguration Configuration { get; private set; }

        public void Configure(ApiConfiguration config)
        {
            this.Configuration = config;
        }
    }
}