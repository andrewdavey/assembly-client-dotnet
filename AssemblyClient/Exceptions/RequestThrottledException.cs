using System;

namespace AssemblyClient
{
    public class RequestThrottledException : Exception
    {
        public RequestThrottledException(string message)
            : base(message)
        {
        }
    }
}
