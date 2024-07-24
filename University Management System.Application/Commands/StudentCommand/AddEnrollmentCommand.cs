using MediatR;

namespace University_Management_System.Application.Commands.StudentCommand;

public class AddEnrollmentCommand : IRequest
{
    public long StudentId { get; set; }
    public long ClassId { get; set; }
}   