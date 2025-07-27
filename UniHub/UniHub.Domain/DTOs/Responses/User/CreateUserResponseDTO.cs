namespace UniHub.Domain.DTOs.Responses.User;

public record CreateUserResponseDTO
(
    long? Id,
    string? ExternalIdentifier,
    string? Name,
    string? Role
);
