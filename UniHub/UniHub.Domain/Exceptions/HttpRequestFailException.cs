using System.Net;

namespace UniHub.Domain.Exceptions;

public class HttpRequestFailException : BaseException
{
    public HttpRequestFailException(string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string? correlationId = null)
        : base(errorCode, message, statusCode, correlationId)
    {
    }
}
