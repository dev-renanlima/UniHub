using System.Net;

namespace UniHub.Domain.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode;
    public string? ErrorCode { get; }
    public string? CorrelationId { get; }

    public BaseException(string errorCode, string message, HttpStatusCode statusCode, string? correlationId = null)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
        CorrelationId = correlationId ?? Guid.NewGuid().ToString("N");
    }
}
