using System;

namespace AssemblyClient 
{
    public class ApiConfiguration
    {
        public string Token { get; set; }
        
        public string RefreshToken { get; set; }
        
        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }

        internal string BasicAuth 
        { 
            get 
            {
                var credentials = $"{ClientId}:{ClientSecret}";
                var bytes = Portable.Text.ASCIIEncoding.ASCII.GetBytes(credentials);
                var authString = Convert.ToBase64String(bytes);
                return authString;
            }
        }
    }
}