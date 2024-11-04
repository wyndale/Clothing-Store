using ClothingStore.IdentityService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClothingStore.IdentityService.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly string _issuer;

    public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer is missing");

        _userManager = userManager;
    }

    public async Task<string> GenerateTokenAsync(IdentityUser user)
    {
        var role = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new Exception("User doesn't exist.")),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var roles in role)
        {
            claims.Add(new Claim(ClaimTypes.Role, roles));
        }

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now,
            Expires = now.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = _issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
