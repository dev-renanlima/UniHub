namespace UniHub.Domain.DTOs.Responses.User;

public record GetUserResponseDTO
(
    string? InternalIdentifier,
    string? ExternalIdentifier,
    string? Name,
    string? Email,
    string? Role,
    string? Status,
    string? ProfileUrl
);
