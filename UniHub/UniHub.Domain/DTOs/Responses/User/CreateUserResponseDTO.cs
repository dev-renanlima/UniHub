namespace UniHub.Domain.DTOs.Responses.User;

public record CreateUserResponseDTO
(
    string? InternalIdentifier,
    string? ExternalIdentifier,
    string? Name,
    string? Email,
    int? Role,
    int? Status,
    string? ProfileUrl,
    DateTime? CreationDate
);
