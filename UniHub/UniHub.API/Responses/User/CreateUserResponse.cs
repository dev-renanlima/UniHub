using UniHub.Domain.Enums;

namespace UniHub.API.Responses.User;

public record CreateUserResponse(
    string InternalIdentifier,
    string ExternalIdentifier,
    string Name,
    string Email,
    UserRole Role,
    UserStatus Status,
    string? ProfileUrl,
    DateTime CreationDate
);
