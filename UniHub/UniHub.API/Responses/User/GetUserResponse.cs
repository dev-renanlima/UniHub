using UniHub.Domain.Enums;

namespace UniHub.API.Responses;

public record GetUserResponse
(
    string InternalIdentifier,
    string ExternalIdentifier,
    string Name,
    string Email,
    UserRole Role,
    UserStatus Status,
    string? ProfileUrl,
    DateTime CreationDate,
    DateTime? UpdateDate
);

