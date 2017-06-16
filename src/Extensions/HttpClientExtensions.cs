using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public static class HttpClientExtensions
    {
        private static void SetAuthorizationHeader(this HttpClient me, string token)
        {
            me.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<HttpResponseMessage> MakeRequest(this HttpClient me, string resourceWithQuery, string token)
        {
            me.SetAuthorizationHeader(token);
            var response = await me.GetAsync(resourceWithQuery);
            return response;
        }

        public static async Task<HttpResponseMessage> PostData(this HttpClient me, string uri, string token, object data)
        {
            me.SetAuthorizationHeader(token);
            var serialized = JsonConvert.SerializeObject(data);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var response = await me.PostAsync(uri, content);
            return response;
        }
    }
}