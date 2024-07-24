using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Queries.TeacherQuery;

public class GetTeacherByIdQuery : IRequest<Teacher>
{
    public long TeacherId { get; set; }
}
