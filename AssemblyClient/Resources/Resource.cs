namespace AssemblyClient
{
    public class Resource
    {
        protected ApiClient Client { get; private set; }

        internal static string DateFormat => "yyyy-MM-dd";

        public Resource(ApiClient client)
        {
            Client = client;
        }
    }
}