using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class SchoolDetailsResource : Resource
    {
        public static string ResourceName => "school_details";

        public SchoolDetailsResource(ApiClient client)
            : base(client)
        {
        }

        public async Task<School> Details()
        {
            dynamic args = new ExpandoObject();
            var result = await Client.GetObject<School>(ResourceName, args);

            return result;
        }
    }
}