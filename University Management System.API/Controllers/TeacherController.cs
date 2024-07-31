using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using University_Management_System.Domain.Models;
using Microsoft.AspNetCore.OData.Query;
using University_Management_System.Application.Commands.TeacherCommand;
using University_Management_System.Application.Queries.TeacherQuery;

namespace University_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            var query = new GetAllTeachersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> GetTeacherById(long id)
        {
            var query = new GetTeacherByIdQuery { TeacherId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("AddCourse")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("AddSession")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddSession([FromBody] AddSessionCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}