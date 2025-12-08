using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Security;
using Application.Security;
using Application.User.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public class TokenCreator(IConfiguration conf) : ITokenCreator
{
//#TODO: Error handling
    private readonly string _secretKey = conf["JwtSettings:Key"] 
        ?? throw new InvalidOperationException("Configuration must be provided");

    private readonly string _issuer = conf["JwtSettings:Issuer"] 
        ?? throw new InvalidOperationException("Configuration must be provided");

    private readonly string _audience = conf["JwtSettings:Audience"] 
        ?? throw new InvalidOperationException("Configuration must be provided");

    public string CreateAccessToken(TokenClaims claims, DateTime expiresIn)
    {
        var token = new JwtSecurityToken(
            issuer: _issuer, audience: _audience, expires: expiresIn,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateRefreshToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());

    public string HashToken(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}