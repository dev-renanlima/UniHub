using System.Text.Json.Serialization;

namespace UniHub.Domain.DTOs.Responses.User;

public record GetUserResponseDTO
{
    [JsonIgnore]
    public Guid? Id { get; init; }

    public string? InternalIdentifier { get; init; }
    public string? ExternalIdentifier { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public int? Role { get; init; }
    public int? Status { get; init; }
    public string? ProfileUrl { get; init; }
    public DateTime? CreationDate { get; init; }
    public DateTime? UpdateDate { get; init; }
}

