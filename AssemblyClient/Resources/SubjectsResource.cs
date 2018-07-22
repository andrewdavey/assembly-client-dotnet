using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class SubjectsResource : ListResource<Subject>
    {
        public static string ResourceName => "subjects";

        public SubjectsResource(ApiClient client)
            : base(client, ResourceName)
        {
        }
    }
}