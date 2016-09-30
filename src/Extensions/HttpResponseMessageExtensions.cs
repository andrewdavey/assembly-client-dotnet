using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Portable.Text;

namespace AssemblyClient
{

    public class ApiGrant
    {
        [JsonProperty("grant_type")]
        public string GrantType { get { return "refresh_token"; } }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public ApiGrant(string refreshToken)
        {
            this.RefreshToken = refreshToken;
        }
    }
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

                var tokenValid = (error.error != "invalid_token");

                return tokenValid;
            }

            return true;
        }
    }

    public static class ClientExtensions
    {
        public static async Task<HttpResponseMessage> MakeRequest(this HttpClient me, string resourceWithQuery, string token)
        {
            me.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await me.GetAsync(resourceWithQuery);
            return response;
        }
    }
}