namespace UniHub.Domain.DTOs.Responses.User
{
    public record CreateUserResponseDTO(
        string? Name,
        string? Email,
        string? Role,
        DateTime? CreationDate
    );
}
