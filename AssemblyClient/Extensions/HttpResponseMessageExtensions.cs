using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public static class HttpResponseMessageExtensions
    {
        public static Task EnsurePlatformSuccess(this HttpResponseMessage me)
        {
            if (me.StatusCode == (HttpStatusCode)429)
            {
                throw new RequestThrottledException("Request throttled");
            }

            return me.EnsureSuccessfulStatusCode();
        }

        public static async Task EnsureSuccessfulStatusCode(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = string.Empty;

            if (response.Content != null)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            if (response.Content != null)
            {
                response.Content.Dispose();
            }

            throw new HttpResponseDetailedException(response.StatusCode, content);
        }

        public static async Task<dynamic> Deserialize(this HttpResponseMessage me)
        {
            return await me.Deserialize<ExpandoObject>();
        }

        public static async Task<T> Deserialize<T>(this HttpResponseMessage me)
        {
            var result = await me.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(result);
            return data;
        }

        public static async Task<bool> IsValidToken(this HttpResponseMessage me)
        {
            if (me.StatusCode == HttpStatusCode.Unauthorized)
            {
                var error = await me.Deserialize();

                var tokenValid = error.error != "invalid_token";

                return tokenValid;
            }

            return true;
        }
    }
}
