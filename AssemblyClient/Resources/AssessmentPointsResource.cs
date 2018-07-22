using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class AssessmentPointsResource : ListResource<AssessmentPoint>
    {
        public static string ResourceName => "assessment_points";

        public AssessmentPointsResource(ApiClient client)
            : base(client, ResourceName)
        {
        }
    }
}