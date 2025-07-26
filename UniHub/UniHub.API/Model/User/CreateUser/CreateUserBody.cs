using UniHub.Domain.Enums;

namespace UniHub.API.Model.User.CreateUser
{
    public class CreateUserBody
    {
        public string? ClerkId { get; set; }

        public string? Name { get; set; }

        public int? Role { get; set; }
    }
}
