namespace UniHub.Domain.DTOs.Responses.User
{
    public record GetUserResponseDTO(
        string? ClerkId,
        string? Name,
        string? Role
    );
}
