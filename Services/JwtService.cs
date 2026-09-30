namespace CrudAPi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrudAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Models;

public interface IJwtService
{
    string GenerateToken(User user);
}
public class JwtService: IJwtService
{
    private readonly IConfiguration _config;
    private readonly int _accessTokenExpiryMinutes;
    public JwtService(IConfiguration config)
    {
        _config = config;
        _accessTokenExpiryMinutes = config.GetValue<int>("jwt:ExpireTime");
    }
    public string  GenerateToken(User user)
    { 
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_accessTokenExpiryMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
};