using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Application.Commands.StudentCommand;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;
using University_Management_System.Persistence;


public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, long>
{
    private readonly UmsContext _context;

    public AddStudentCommandHandler(UmsContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var roleExists = await _context.Roles.AnyAsync(r => r.Id == 3, cancellationToken);
        if (!roleExists)
        {
            throw new KeyNotFoundException("The specified role does not exist.");
        }
        var student = new Student
        {
            Name = request.Name,
            RoleId = 3,
            Email = request.Email,
            CanApplyToFrance = request.CanApplyToFrance,
            AverageGrade = request.AverageGrade,
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student.Id;
    }
}
