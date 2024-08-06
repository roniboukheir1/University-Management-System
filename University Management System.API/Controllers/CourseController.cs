using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using University_Management_System.Application.Commands.CourseCommand;
using University_Management_System.Application.Queries.CourseQueries;
using University_Management_System.Application.Services;
using University_Management_System.Domain.Models;

namespace University_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator, CoursePublisher publisher)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var courseId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseId }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(long id)
        {
            var query = new GetCourseByIdQuery { CourseId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}