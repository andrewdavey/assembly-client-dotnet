namespace AssemblyClient
{
    public class Resource
    {
        internal static string DateFormat => "yyyy-MM-dd";

        public ApiClient Client { get; }

        public Resource(ApiClient client)
        {
            this.Client = client;
        }
    }
}