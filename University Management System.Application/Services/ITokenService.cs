using University_Management_System.API.Settings;

namespace University_Management_System.Application.Services;

public interface ITokenService
{
    public string GenerateJwt(JWTSettings jwtSettings, string[] permissions);
}