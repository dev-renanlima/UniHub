using UniHub.Domain.Enums;

namespace UniHub.API.Requests;

public record CreateUserRequest
(
    string ExternalIdentifier,
    string Name,
    string Email,
    UserRole Role,
    string? ProfileUrl
);
