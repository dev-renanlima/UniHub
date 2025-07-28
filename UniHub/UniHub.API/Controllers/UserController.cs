using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniHub.API.Model.User.CreateUser;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;
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

    [HttpPost("/createUser")]
    public async Task<IActionResult> CreateUser(CreateUserModel createUserModel)
    {
        UserDTO userDTO = createUserModel.Adapt<UserDTO>();

        CreateUserResponseDTO response = await _userService.CreateAsync(userDTO);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("/getUserByExternalIdentifier/{externalIdentifier}")]
    public async Task<IActionResult> GetUserByExternalIdentifier([FromRoute] string externalIdentifier)
    {
        GetUserResponseDTO? response = await _userService.GetUserByExternalIdentifierAsync(externalIdentifier);

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
