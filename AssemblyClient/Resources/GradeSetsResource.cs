using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class GradeSetsResource : ListResource<GradeSet>
    {
        public static string ResourceName => "grade_sets";

        public GradeSetsResource(ApiClient client)
            : base(client, ResourceName)
        {
        }
    }
}