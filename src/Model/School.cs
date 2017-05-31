using Newtonsoft.Json;

namespace AssemblyClient
{
    public class School
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("la_code")]
        public string LocalAuthorityCode { get; set; }

        [JsonProperty("la_name")]
        public string LocalAuthorityName { get; set; }

        [JsonProperty("establishment_number")]
        public string EstablishmentNumber { get; set; }

        [JsonProperty("establishment_type")]
        public string EstablishmentType { get; set; }

        [JsonProperty("phase")]
        public string Phase { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("head_teacher")]
        public string HeadTeacher { get; set; }
    }
}
