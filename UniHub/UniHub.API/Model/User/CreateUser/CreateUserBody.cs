using UniHub.Domain.Enums;

namespace UniHub.API.Model.User.CreateUser
{
    public class CreateUserBody
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public int? Role { get; set; }
    }
}
