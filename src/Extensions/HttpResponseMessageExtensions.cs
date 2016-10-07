using System.Dynamic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace AssemblyClient
{
    public static class HttpResponseMessageExtensions
    {
        public static void EnsurePlatformSuccess(this HttpResponseMessage me)
        {
            if (me.StatusCode == (HttpStatusCode)429)
            {
                throw new RequestThrottledException("Request throttled");
            }

            me.EnsureSuccessfulStatusCode();
        }

        public static void EnsureSuccessfulStatusCode(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = string.Empty;

            if (response.Content != null)
            {
                content = response.Content.ReadAsStringAsync().Result;
            }

            if (response.Content != null)
            {
                response.Content.Dispose();
            }

            throw new HttpResponseDetailedException(response.StatusCode, content);
        }

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
