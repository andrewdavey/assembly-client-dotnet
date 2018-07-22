using Newtonsoft.Json;

namespace AssemblyClient
{
    public class StudentDemographics
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("is_pp")]
        public bool? PupilPremium { get; set; }

        [JsonProperty("ethnicity_code")]
        public string EthnicityCode { get; set; }

        [JsonProperty("ethnicity_group")]
        public string EthnicityGroup { get; set; }

        [JsonProperty("is_eal")]
        public bool? EnglishAdditionalLanguage { get; set; }

        [JsonProperty("is_fsm")]
        public bool? FreeSchoolMeal { get; set; }

        [JsonProperty("sen_category")]
        public string SenCategory { get; set; }

        [JsonProperty("in_care")]
        public bool? InCare { get; set; }
    }
}
