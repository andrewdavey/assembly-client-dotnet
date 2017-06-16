using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class AssessmentsResource : ListResource<Assessment>
    {
        public static string ResourceName => "assessments";

        public AssessmentsResource(ApiClient client)
            : base(client, ResourceName)
        {
        }
    }
}