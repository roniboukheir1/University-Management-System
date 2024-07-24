using MediatR;

namespace University_Management_System.Application.Commands.TeacherCommand;

public class AddCourseCommand : IRequest
{
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
}