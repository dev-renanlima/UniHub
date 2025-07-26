using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using UniHub.Domain.Interfaces.Services;
using UniHub.API.Model.User.CreateUser;
using UniHub.Domain.DTOs;
using Mapster;
using UniHub.Domain.DTOs.Responses.User;

namespace UniHub.API.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("unihub/api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateUser(CreateUserModel createUserModel)
        {
            UserDTO userDTO = createUserModel.Adapt<UserDTO>();

            CreateUserResponseDTO response = await _userService.Create(userDTO);

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
