using MediatR;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Queries.StudentQuery;

public class GetStudentByIdQuery : IRequest<Student>
{
    public long StudentId { get; set; }

    public GetStudentByIdQuery(long studentId)
    {
        StudentId = studentId;
    }
}