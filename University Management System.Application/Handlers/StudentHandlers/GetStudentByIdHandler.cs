using MediatR;
using University_Management_System.Application.Queries.StudentQuery;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.StudentHandlers;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Student>
{

    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdHandler(IStudentRepository studentRepo)
    {
        _studentRepository = studentRepo;
    }


    public Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetByIdAsync(request.StudentId);
        return student;
    }
}