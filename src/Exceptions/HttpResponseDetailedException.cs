using System;
using System.Net;

namespace AssemblyClient
{
    public class HttpResponseDetailedException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpResponseDetailedException(HttpStatusCode statusCode, string content)
            : base(content)
        {
            StatusCode = statusCode;
        }
    }
}