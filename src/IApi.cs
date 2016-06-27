using System.Collections.Generic;

namespace AssemblyClient
{   
    public interface IApi
    {
        IList<T> get<T>(string resource, ApiConfiguration config);   
    }
}