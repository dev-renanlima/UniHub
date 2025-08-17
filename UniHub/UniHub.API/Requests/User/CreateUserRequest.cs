using UniHub.Domain.Enums;

namespace UniHub.API.Requests.User;

public record CreateUserRequest(
    string ExternalIdentifier,
    string Name,
    string Email,
    UserRole Role,
    string? ProfileUrl
);
