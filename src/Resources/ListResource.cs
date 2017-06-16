using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class ListResource<T> : Resource
    {
        private string resourceName;

        public ListResource(ApiClient client, string resourceName)
            : base(client)
        {
            this.resourceName = resourceName;
        }

        public async Task<IList<T>> All()
        {
            var results = await List(perPage: 100);
            return results;
        }

        public async Task<IList<T>> List(int? perPage = null)
        {
            dynamic args = new ExpandoObject();

            args.perPage = perPage;

            var results = await Client.GetList<T>(resourceName, args);

            return results;
        }
    }
}