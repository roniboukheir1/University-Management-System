using MediatR;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Application.Events;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Handlers.GradeHandlers
{
    public class GradeSetEventHandler : INotificationHandler<GradeSetEvent>
    {
        private readonly UmsContext _context;

        public GradeSetEventHandler(UmsContext context)
        {
            _context = context;
        }

        public async Task Handle(GradeSetEvent notification, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(s => s.StudentCourseGrades)
                .FirstOrDefaultAsync(s => s.Id == notification.StudentId, cancellationToken);

            if (student != null)
            {
                student.AverageGrade = (double) student.StudentCourseGrades.Average(g => g.Grade);
                student.CanApplyToFrance = student.AverageGrade > 15;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}