using Microsoft.AspNetCore.Mvc;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Services;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
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
    [HttpPost("{studentId}/courses/{courseId}/grade")]
    public IActionResult SetGrade(long studentId, long courseId, [FromBody] GradeRequest request)
    {
        if (request == null || request.Grade < 0 || request.Grade > 20)
        {
            return BadRequest("Invalid grade");
        }
        try
        {
            _teacherServices.SetGrade(studentId, courseId, request.Grade);
            return Ok("Grade set successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /*[HttpPost("{studentId}/profile-picture")]
    public IActionResult UploadProfilePicture(long studentId, [FromForm] IFormFile profilePicture)
    {
        try
        {
            _teacherServices.UploadProfilePicture(studentId, profilePicture);
            return Ok("Profile picture uploaded successfully");
        }
        catch (Exception ex)
        {
           return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }*/
    public class GradeRequest
    {
        public double Grade { get; set; }
    }
}