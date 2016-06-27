using System.Collections.Generic;

namespace AssemblyClient
{   
    public class ApiList<T>
    {
        public int total_count { get; set; }

        public int total_pages { get; set; }

        public int? current_page { get; set; }

        public int? next_page { get; set; }

        public List<T> data { set; get; }

        public ApiList() 
        {
          this.data = new List<T>();
        }
    }
}