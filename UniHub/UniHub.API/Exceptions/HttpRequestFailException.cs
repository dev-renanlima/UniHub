using Microsoft.AspNetCore.Http;
using System.Net;

namespace UniHub.API.Exceptions
{
    public class HttpRequestFailException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public Exception? RegularException { get; }

        public string? FormData { get; }

        public string? ErrorCode { get; }

        public string? CorrelationId { get; }

        public HttpRequestFailException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpRequestFailException(Exception ex, string data, HttpStatusCode statusCode)
        {
            RegularException = ex;
            FormData = data;
            StatusCode = statusCode;
        }

        public HttpRequestFailException(string message, HttpStatusCode statusCode, string? correlationId = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = statusCode.ToString();
            CorrelationId = correlationId ?? Guid.NewGuid().ToString("N");
        }
    }
}