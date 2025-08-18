using System.Net;

namespace UniHub.Application.Exceptions;

public class ApiKeyAuthException : Exception
{
    public readonly HttpStatusCode StatusCode;
    public string? ErrorCode { get; }
    public string? CorrelationId { get; }

    public ApiKeyAuthException(string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.Unauthorized, string? correlationId = null)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
        CorrelationId = correlationId ?? Guid.NewGuid().ToString("N");
    }
}
