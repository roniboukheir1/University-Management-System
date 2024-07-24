using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Commands.TeacherCommand;

public class AddSessionCommand : IRequest
{
    public long ClassId { get; set; }
    public SessionTime SessionTime { get; set; }
}