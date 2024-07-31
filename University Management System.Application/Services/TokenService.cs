using System.Security.Claims;
using System.Text;
using University_Management_System.API.Settings;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace University_Management_System.Application.Services;


public class TokenService : ITokenService
{
    public string GenerateJwt(JWTSettings jwtSettings, string[] permissions)
    {
        
        var keyBytes = Encoding.UTF8.GetBytes("razlcGHL09GOx1vTItRfKlbvO8icZb0N"); // converts the Secret key into a byte array
      
        var symmetricKey = new SymmetricSecurityKey(keyBytes); // used to sign the JWT, ensures that the key used for signing the token is secure and symmetric
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256); // specifies the algo and key to use for signing the JWT

        var claims = new List<Claim>()
        {
            new Claim("myclaim", "Some extra data")
        };

        var roleClaims = permissions.Select(x => new Claim(ClaimTypes.Role, x));
        claims.AddRange(roleClaims);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(jwtSettings.ExpirationSecond),
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        return rawToken;
    }
}
