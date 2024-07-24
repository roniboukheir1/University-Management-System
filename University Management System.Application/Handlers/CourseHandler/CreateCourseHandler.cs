using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University_Management_System.Application.Commands.CourseCommand;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;
using University_Management_System.Persistence;

namespace University_Management_System.Application.Handlers.CourseHandlers;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, long>
{
    private readonly UmsContext _context;

    public CreateCourseCommandHandler(UmsContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Name = request.Name,
            MaxStudentsNumber = request.MaxStudentsNumber,
            EnrolmentDateRange = request.EnrollmentDateRange
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return course.CourseId;
    }
}