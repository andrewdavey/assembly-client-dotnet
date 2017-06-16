using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class AspectsResource : ListResource<Aspect>
    {
        public static string ResourceName => "aspects";

        public AspectsResource(ApiClient client)
            : base(client, ResourceName)
        {
        }
    }
}