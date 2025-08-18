using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniHub.API.Requests;
using UniHub.API.Responses;
using UniHub.Domain.DTOs;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.API.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("unihub/api/v{version:apiVersion}/")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public LoginController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Autenticar usuário",
        Description = "Gera um token JWT válido para acessar endpoints protegidos"
    )]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        UserDTO userDTO = await _userService.GetUserByIdentifierAsync(request.ExternalIdentifier);

        string token = _jwtService.GenerateToken(userDTO);

        LoginResponse response = token.Adapt<LoginResponse>();

        return StatusCode(StatusCodes.Status202Accepted, response);
    }

    [HttpPost("me")]
    [SwaggerOperation(
        Summary = "Usuário autenticado",
        Description = "Obtém o usuário que está autenticado"
    )]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> Me([FromBody] LoginRequest request)
    {
        UserDTO userDTO = await _userService.GetUserByIdentifierAsync(request.ExternalIdentifier);

        string token = _jwtService.GenerateToken(userDTO);

        LoginResponse response = token.Adapt<LoginResponse>();

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
