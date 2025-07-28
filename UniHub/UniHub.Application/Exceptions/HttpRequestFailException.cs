using System.Net;

namespace UniHub.Application.Exceptions
{
    public class HttpRequestFailException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public Exception? RegularException { get; }

        public string? FormData { get; }

        public string? ErrorCode { get; }

        public string? CorrelationId { get; }

        public HttpRequestFailException(Exception ex, string data, HttpStatusCode statusCode)
        {
            RegularException = ex;
            FormData = data;
            StatusCode = statusCode;
        }

        public HttpRequestFailException(string errorCode, string message, HttpStatusCode statusCode, string? correlationId = null)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            CorrelationId = correlationId ?? Guid.NewGuid().ToString("N");
        }
    }
}