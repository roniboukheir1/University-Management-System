using MediatR;

namespace University_Management_System.Application.Commands.GradeCommands
{
    public class SetGradeCommand : IRequest
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public double Grade { get; set; }
    }
}