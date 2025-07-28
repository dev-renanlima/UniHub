namespace UniHub.Domain.DTOs.Responses.User;

public record GetUserResponseDTO
(
    long? Id,
    string? ExternalIdentifier,
    string? Name,
    string? Role
);
