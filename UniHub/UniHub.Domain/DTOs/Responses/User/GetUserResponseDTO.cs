namespace UniHub.Domain.DTOs.Responses.User;

public record GetUserResponseDTO
(
    long? Id,
    string? ClerkId,
    string? Name,
    string? Role
);
