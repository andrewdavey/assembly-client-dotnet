using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class SchoolDetailsResource : Resource
    {
        public static string ResourceName => "school_details";

        private readonly ApiClient client;

        public SchoolDetailsResource(ApiClient client)
        {
            this.client = client;
        }

        public async Task<School> Details()
        {
            dynamic args = new ExpandoObject();
            var result = await client.GetObject<School>(ResourceName, args);

            return result;
        }
    }
}