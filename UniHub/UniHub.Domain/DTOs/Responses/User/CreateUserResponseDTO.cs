namespace UniHub.Domain.DTOs.Responses.User
{
    public record CreateUserResponseDTO(
        string? ClerkId,
        string? Name,
        string? Role
    );
}
