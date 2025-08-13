namespace UniHub.Domain.DTOs.Responses.User;

public record GetUserResponseDTO
(
    long? Id,
    string? InternalIdentifier,
    string? ExternalIdentifier,
    string? Name,
    string? Email,
    int? Role,
    int? Status,
    string? ProfileUrl,
    DateTime? CreationDate,
    DateTime? UpdateDate
);
