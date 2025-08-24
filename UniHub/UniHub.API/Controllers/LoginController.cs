using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using UniHub.API.Requests;
using UniHub.API.Responses;
using UniHub.CrossCutting.Auth;
using UniHub.Domain.DTOs;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.API.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("unihub/api/v{version:apiVersion}/")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public LoginController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Autenticar usuário",
        Description = "Gera um token JWT válido para acessar endpoints protegidos"
    )]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemResponse), StatusCodes.Status404NotFound)]
    [Authorize(Policy = AuthPolicies.ApiKey)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        UserDTO userDTO = await _userService.GetUserByIdentifierAsync(request.ExternalIdentifier);

        GetUserResponse userResponse = userDTO.Adapt<GetUserResponse>();

        string token = _tokenService.GenerateToken(userDTO);

        LoginResponse response = (userResponse, token).Adapt<LoginResponse>();
        
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("me")]
    [SwaggerOperation(
        Summary = "Usuário autenticado",
        Description = "Obtém o usuário que está autenticado"
    )]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemResponse), StatusCodes.Status401Unauthorized)]    
    [Authorize(Policy = AuthPolicies.ApiKeyAndJwt)]
    public async Task<IActionResult> Me()
    {
        string? internalIdentifier = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        UserDTO userDTO = await _userService.GetUserByIdentifierAsync(internalIdentifier!);

        GetUserResponse response = userDTO.Adapt<GetUserResponse>();

        return StatusCode(StatusCodes.Status200OK, response);
    }
}
