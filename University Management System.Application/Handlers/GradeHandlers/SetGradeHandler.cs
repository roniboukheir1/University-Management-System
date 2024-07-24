using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University_Management_System.Application.Commands.GradeCommands;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence;
using University_Management_System.Application.Events;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Handlers.GradeHandlers
{
    public class SetGradeCommandHandler : IRequestHandler<SetGradeCommand>
    {
        private readonly UmsContext _context;
        private readonly IMediator _mediator;

        public SetGradeCommandHandler(UmsContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(SetGradeCommand request, CancellationToken cancellationToken)
        {
            var studentCourseGrade = new StudentCourseGrade
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                Grade = request.Grade
            };

            _context.StudentCourseGrades.Add(studentCourseGrade);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new GradeSetEvent(request.StudentId), cancellationToken);

        }
    }
}