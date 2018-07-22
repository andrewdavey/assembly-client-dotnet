using Newtonsoft.Json;

namespace AssemblyClient
{
    internal class ApiGrant
    {
        [JsonProperty("grant_type")]
        public string GrantType
        {
            get { return "refresh_token"; }
        }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public ApiGrant(string refreshToken)
        {
            this.RefreshToken = refreshToken;
        }
    }
}