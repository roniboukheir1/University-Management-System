using University_Management_System.API.Settings;

namespace University_Management_System.Application.Services;

public class AuthenticationService(ITokenService tokenService, JWTSettings jwtSettings) : IAuthenticationService
{
    public string Login(string username, string password)
    {
        //check if the username exists in the database return the token
        return tokenService.GenerateJwt(jwtSettings, [ "Teacher"]);
    }
}