using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Queries.CourseQueries;

public class GetCourseByIdQuery : IRequest<Course>
{
    public long CourseId { get; set; }
}