using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class StaffMembersResource : Resource
    {
        public const string ResourceName = "staff_members";

        private readonly ApiClient client;

        public StaffMembersResource(ApiClient client)
        {
            this.client = client;
        }

        public Task<IList<StaffMember>> All()
        {
            var results = List(perPage: 100);
            return results;
        }

        public async Task<IList<StaffMember>> List(DateTime? date = null, bool? teachersOnly = false, int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.date = date?.ToString(DateFormat);
            args.teachers_only = teachersOnly;
            args.perPage = perPage;

            var results = await client.GetList<StaffMember>(ResourceName, args);
            return results;
        }
    }
}
