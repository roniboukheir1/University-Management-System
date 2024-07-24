using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Queries.TeacherQuery;

public class GetAllTeachersQuery : IRequest<IEnumerable<Teacher>>
{
}