using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniHub.API.Requests.User;
using UniHub.API.Responses.User;
using UniHub.Domain.DTOs;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.API.Controllers;

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

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        UserDTO userDTO = request.Adapt<UserDTO>();

        UserDTO createdUser = await _userService.CreateAsync(userDTO);

        CreateUserResponse response = createdUser.Adapt<CreateUserResponse>();

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("{identifier}")]
    public async Task<IActionResult> GetUserByIdentifier([FromRoute] string identifier)
    {
        UserDTO userDTO = await _userService.GetUserByIdentifierAsync(identifier);

        CreateUserResponse response = userDTO.Adapt<CreateUserResponse>();

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
