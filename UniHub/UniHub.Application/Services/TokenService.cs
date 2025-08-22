using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniHub.Domain.DTOs;
using UniHub.Domain.Interfaces.Services;
using UniHub.Domain.Options;

namespace UniHub.Application.Services;

public class TokenService : ITokenService
{
    private readonly SecurityOptions _securityOptions;

    public TokenService(IOptions<SecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    public string GenerateToken(UserDTO user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityOptions.SecretKey!));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expirationTime = double.Parse(_securityOptions.ExpirationTime!);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.InternalIdentifier),
            new Claim("externalIdentifier", user.ExternalIdentifier),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _securityOptions.Issuer,
            audience: _securityOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expirationTime),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}