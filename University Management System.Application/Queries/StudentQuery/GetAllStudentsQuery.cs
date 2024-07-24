using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Queries.StudentQuery;

public class GetAllStudentsQuery : IRequest<IEnumerable<Student>>
{
    public GetAllStudentsQuery()
    {
        
    }
}