using System.Collections.Generic;
using AssemblyClient;

namespace AllTheTests 
{
    public class MockApi : IApi
    {
        public IList<T> get<T>(string resource, ApiConfiguration config)
        {
            var results = new List<T>();
            results.Add(default(T));
            results.Add(default(T));
            return results;
        }
    }
}