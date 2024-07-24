using MediatR;
using Microsoft.AspNetCore.Mvc;
using University_Management_System.Application.Commands.StudentCommand;
using University_Management_System.Application.Handlers.StudentHandlers;
using University_Management_System.Application.Queries.StudentQuery;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class StudentController : ControllerBase
{
   private readonly IMediator _mediator;
   public StudentController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpPost("Enroll")]
   public async Task<IActionResult> AddEnrollment([FromQuery] AddEnrollmentCommand command)
   {
      await _mediator.Send(command);
      return Ok();
   }

   [HttpGet("students/{id}")]
   public async Task<IActionResult> GetById(long id)
   {
      var query = new GetStudentByIdQuery(id);
      var student = await _mediator.Send(query);
      if (student == null)
      {
         return NotFound();
      }

      return Ok(student);
   }

   [HttpGet("students")]
   public async Task<IActionResult> GetAll()
   {
      var query = new GetAllStudentsQuery();
      return Ok(await _mediator.Send(query));
   }
}
