using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public static class HttpResponseMessageExtensions
    {
        public static dynamic Deserialize(this HttpResponseMessage me)
        {
            var result = me.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<ExpandoObject>(result);
            return data;
        }

        public static bool IsValidToken(this HttpResponseMessage me)
        {
            if (me.StatusCode == HttpStatusCode.Unauthorized)
            {
                var error = me.Deserialize();

                var tokenValid = error.error != "invalid_token";

                return tokenValid;
            }

            return true;
        }
    }
}
