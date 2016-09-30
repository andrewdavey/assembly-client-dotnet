using Newtonsoft.Json;

namespace AssemblyClient
{   
    public class Student
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public string DOB { get; set; }

        [JsonProperty("year_code")]
        public string YearCode { get; set; }

        [JsonProperty("upn")]
        public string UPN { get; set; }
    }
}