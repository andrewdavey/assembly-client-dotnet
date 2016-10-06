using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> MakeRequest(this HttpClient me, string resourceWithQuery, string token)
        {
            me.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await me.GetAsync(resourceWithQuery);
            return response;
        }
    }
}