using System.Net;

namespace UniHub.Domain.Exceptions;

public class JwtAuthException : BaseException
{
    public JwtAuthException(string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.Unauthorized, string? correlationId = null)
        : base(errorCode, message, statusCode, correlationId)
    {
    }
}

