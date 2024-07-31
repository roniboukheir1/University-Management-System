namespace University_Management_System.Application.Services;

public interface IAuthenticationService
{
    public string Login(string username, string password);
}