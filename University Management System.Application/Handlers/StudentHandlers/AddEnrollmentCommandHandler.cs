using MediatR;
using University_Management_System.Application.Commands.StudentCommand;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.StudentHandlers;

public class AddEnrollmentCommandHandler : IRequestHandler<AddEnrollmentCommand>
{

    public readonly IStudentRepository _StudentRepository;
    public readonly IClassRepository _ClassRepository;
    
    public AddEnrollmentCommandHandler(IStudentRepository studentRepository, IClassRepository classRepository)
    {
        _StudentRepository = studentRepository;
        _ClassRepository = classRepository;
    }
    
    public async Task Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var student = await _StudentRepository.GetByIdAsync(request.StudentId);
        if (student == null)
        {
            throw new NotFoundException("Student Not Found");
        }

        var @class = await _ClassRepository.GetByIdAsync(request.ClassId);
        if (@class == null)
        {
            throw new NotFoundException("Class Not Found");
        }
        var enrollment = new ClassEnrollment
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId
        };
        await _StudentRepository.AddEnrollmentAsync(enrollment);
    }   
}