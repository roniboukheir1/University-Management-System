using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using University_Management_System.Application.Commands.GradeCommands;

namespace University_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SetGrade([FromBody] SetGradeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}