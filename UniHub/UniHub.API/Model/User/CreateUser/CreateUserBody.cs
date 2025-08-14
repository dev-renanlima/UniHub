using UniHub.Domain.Enums;

namespace UniHub.API.Model.User.CreateUser;

public class CreateUserBody
{
    public required string ExternalIdentifier { get; set; } 

    public required string Name { get; set; } 

    public required string Email { get; set; }

    public int Role { get; set; }

    public string? ProfileUrl { get; set; }
}
