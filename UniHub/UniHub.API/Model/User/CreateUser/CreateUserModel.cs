using Microsoft.AspNetCore.Mvc;

namespace UniHub.API.Model.User.CreateUser
{
    public class CreateUserModel
    {
        [FromBody]
        public CreateUserBody? Body { get; set; }
    }
}
