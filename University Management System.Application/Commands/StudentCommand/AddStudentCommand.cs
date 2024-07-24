using MediatR;

namespace University_Management_System.Application.Commands.StudentCommand;

public class AddStudentCommand : IRequest<long>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public double AverageGrade { get; set; }
    public bool CanApplyToFrance { get; set; }
}