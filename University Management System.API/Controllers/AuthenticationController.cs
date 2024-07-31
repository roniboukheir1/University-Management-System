/*using Microsoft.AspNetCore.Mvc;
using University_Management_System.Application.Services;
using University_Management_System.Domain.Models.Dtos;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost]
    public IActionResult login([FromBody] LoginDto request)
    {
        return Ok(authenticationService.Login(request.Username, request.Password));
    }
}*/