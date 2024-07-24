using MediatR;

namespace University_Management_System.Application.Commands;

public class SetGradeCommand : IRequest
{
    public double Grade { get; }
    public long StudentId { get; }
    public long CourseId { get; }

    public SetGradeCommand(double grade, long studentId, long courseId)
    {
        Grade = grade;
        StudentId = studentId;
        CourseId = courseId;
    }
    

}