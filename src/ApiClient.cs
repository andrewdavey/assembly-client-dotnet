using System;
using System.Net.Http;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssemblyClient
{
    public class ApiClient
    {
        private readonly Api api;

        public ApiConfiguration Configuration => this.api.Configuration;

        public event EventHandler<TokenRefreshedEventArgs> TokenRefreshed
        {
            add
            {
                lock (api.TokenRefreshed)
                {
                    api.TokenRefreshed += value;
                }
            }

            remove
            {
                lock (api.TokenRefreshed)
                {
                    api.TokenRefreshed -= value;
                }
            }
        }

        public ApiClient()
            : this(AssemblyEnvironment.Production)
        {
        }

        public ApiClient(AssemblyEnvironment environment)
        {
            var endpoint = string.Empty;

            switch (environment)
            {
                case AssemblyEnvironment.Sandbox:
                    endpoint = "https://api-sandbox.assembly.education";
                    break;
                default:
                    endpoint = "https://api.assembly.education";
                    break;
            }

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };

            this.api = new Api(httpClient);
        }

        public ApiClient(Api api)
        {
            this.api = api;
        }

        public virtual async Task<T> PostData<T>(string uri, object data)
        {
            var result = await api.PostData<T>(uri, data);
            return result;
        }

        public virtual async Task<IList<T>> GetList<T>(string resource, ExpandoObject args)
        {
            var results = await api.GetList<T>(resource, args);
            return results;
        }

        public virtual async Task<T> GetObject<T>(string resource, ExpandoObject args)
        {
            var results = await api.GetObject<T>(resource, args);
            return results;
        }

        public virtual void Configure(ApiConfiguration config)
        {
            this.api.Configuration = config;
        }

        public AcademicYearsResource AcademicYears => new AcademicYearsResource(this);

        public AspectsResource Aspects => new AspectsResource(this);

        public AssessmentPointsResource AssessmentPoints => new AssessmentPointsResource(this);

        public AssessmentsResource Assessments => new AssessmentsResource(this);

        public GradeSetsResource GradeSets => new GradeSetsResource(this);

        public RegistrationGroupsResource RegistrationGroups => new RegistrationGroupsResource(this);

        public ResultsResource Results => new ResultsResource(this);

        public SchoolDetailsResource School => new SchoolDetailsResource(this);

        public StaffMembersResource StaffMembers => new StaffMembersResource(this);

        public StudentsResource Students => new StudentsResource(this);

        public SubjectsResource Subjects => new SubjectsResource(this);

        public TeachingGroupsResource TeachingGroups => new TeachingGroupsResource(this);

        public YearGroupsResource YearGroups => new YearGroupsResource(this);
    }
}
