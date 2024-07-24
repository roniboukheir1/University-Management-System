using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using University_Management_System.Application.Commands;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Services;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("admin/[controller]")]
//[Authorize(Roles = "Admin")]
[ApiVersion("1.0")]
public class CourseController : ControllerBase
{

            private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

    [HttpPost("CreateCourse")]
    public IActionResult CreateCourse([FromBody] CreateCourseCommand command)
    {
        var course = _courseService.CreateCourse(command);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [HttpGet("GetAll")]
    public IActionResult GetCourses()
    {
        return Ok(_courseService.GetAll());
    }
    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(long id)
    {
        var course = _courseService.GetById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(long id)
    {
        return Ok(_courseService.GetById(id));
    }
    [HttpPut("UpdateCourse/{id}")]
    public IActionResult UpdateCourse(long id, [FromBody] CreateCourseCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid course data");
        }

        try
        {
            _courseService.Update(id, command);
            return Ok("Course updated successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(long id)
    {
        _courseService.DeleteCourse(id);
        return Ok();
    }
}