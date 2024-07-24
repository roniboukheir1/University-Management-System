using MediatR;
using Microsoft.AspNetCore.Mvc;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models.Request;

namespace University_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("RegisterTeacher")]
        public async Task<IActionResult> RegisterTeacher([FromQuery] TimeSlotModel model)
        {
            try
            {
                var command = new RegisterTeacherCommand(model);
                await _mediator.Send(command);
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
        public async Task<IActionResult> SetGrade(long studentId, long courseId, [FromBody] GradeRequest request)
        {
            if (request == null || request.Grade < 0 || request.Grade > 20)
            {
                return BadRequest("Invalid grade");
            }
            try
            {
                var command = new SetGradeCommand(studentId, courseId, request.Grade);
                await _mediator.Send(command);
                return Ok("Grade set successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid enrollment data");
            }

            try
            {
                var command = new EnrollStudentCommand(request.StudentId, request.CourseId);
                await _mediator.Send(command);
                return Ok("Student enrolled successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
   }
}
