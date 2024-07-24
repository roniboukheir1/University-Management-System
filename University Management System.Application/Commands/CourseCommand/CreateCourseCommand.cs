using MediatR;
using NpgsqlTypes;

namespace University_Management_System.Application.Commands.CourseCommand;

public class CreateCourseCommand : IRequest<long>
{
    public string Name { get; set; }
    public int MaxStudentsNumber { get; set; }
    public NpgsqlRange<DateOnly> EnrollmentDateRange { get; set; }
}