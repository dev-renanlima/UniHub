namespace UniHub.Domain.DTOs.Responses.User;

public record CreateUserResponseDTO
(
    long? Id,
    string? ClerkId,
    string? Name,
    string? Role
);
