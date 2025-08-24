using System.Net;

namespace UniHub.Domain.Exceptions;

public class AuthorizationException : BaseException
{
    public AuthorizationException(string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.Unauthorized, string? correlationId = null)
        : base(errorCode, message, statusCode, correlationId)
    {        
    }
}
