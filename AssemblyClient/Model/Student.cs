using Newtonsoft.Json;
using System;

namespace AssemblyClient
{
    public class Student
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("legal_first_name")]
        public string LegalFirstName { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("legal_last_name")]
        public string LegalLastName { get; set; }

        [JsonProperty("dob")]
        public string DOB { get; set; }

        [JsonProperty("year_code")]
        public string YearCode { get; set; }

        [JsonProperty("upn")]
        public string UPN { get; set; }

        [JsonProperty("former_upn")]
        public string FormerUPN { get; set; }

        [JsonProperty("mis_id")]
        public string MisId { get; set; }

        [JsonProperty("pan")]
        public string PupilAdmissionNumber { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        DateTime? endDate;

        [JsonProperty("end_date")]
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                // The API can return a "default" value of 2079-06-06T23:59:00.000Z.
                // We'll treat this as meaning the pupil hasn't got an end date yet.
                if (value.HasValue && value.Value.Year == 2079)
                {
                    endDate = null;
                }
                else
                {
                    endDate = value;
                }
            }
        }

        [JsonProperty("demographics")]
        public StudentDemographics Demographics { get; }

        public Student()
        {
            this.Demographics = new StudentDemographics();
        }
    }
}
