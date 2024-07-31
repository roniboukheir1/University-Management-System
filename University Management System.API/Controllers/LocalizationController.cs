using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

[ApiController]
[Route("api/[controller]")]
public class LocalizationController : ControllerBase
{
    private readonly IStringLocalizer<LocalizationController> _localizer;

    public LocalizationController(IStringLocalizer<LocalizationController> localizer)
    {
        _localizer = localizer;
    }

    [HttpGet("hello")]
    [Authorize(Roles = "Admin,Student,Teacher")]
    public IActionResult GetHello()
    {
        var message = _localizer["Hello"];
        return Ok(new { message });
    }
}