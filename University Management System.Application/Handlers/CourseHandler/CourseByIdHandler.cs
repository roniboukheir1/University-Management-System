using MediatR;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Application.Queries.CourseQueries;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Handlers.CourseHandlers;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course>
{
    private readonly UmsContext _context;

    public GetCourseByIdQueryHandler(UmsContext context)
    {
        _context = context;
    }
    
    public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);
    }
}