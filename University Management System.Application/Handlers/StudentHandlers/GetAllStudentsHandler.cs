using MediatR;
using University_Management_System.Application.Queries.StudentQuery;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.StudentHandlers;

public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetAllAsync();
    }
}