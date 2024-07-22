using Microsoft.AspNetCore.Mvc;
using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Models;
using University_Management_System.Persistence.Services;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{

    private readonly TeacherServices _teacherServices;

    public TeacherController(TeacherServices teacherServices)
    {
        _teacherServices = teacherServices;
    }

    [HttpPut("RegisterTeacher")]
    public IActionResult RegisterTeacher([FromQuery] TimeSlotModel model)
    {
        try
        {
            _teacherServices.registerTeacher(model);
            return Ok("Success");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}