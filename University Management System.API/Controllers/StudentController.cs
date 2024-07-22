using Microsoft.AspNetCore.Mvc;
using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Services;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(StudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Get()
    {
        return Ok(_studentService.GetAll());
    }

    [HttpPost("Enroll")]
    public IActionResult Enroll([FromBody] EnrollmentRequest request)
    {
        if (request == null)
        {
            _logger.LogError("Enroll called with null request.");
            return BadRequest("Invalid enrollment data");
        }

        try
        {
            _studentService.EnrollStudent(request.StudentId, request.CourseId);
            _logger.LogInformation("Student enrolled successfully.");
            return Ok("Student enrolled successfully");
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "NotFoundException occurred.");
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "InvalidOperationException occurred.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Internal server error.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

public class EnrollmentRequest
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
}
