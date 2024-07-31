using MediatR;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Application.Commands.TeacherCommand;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;
using University_Management_System.Application.Repositories;

namespace University_Management_System.Application.Handlers.TeacherHandlers;

public class AddSessionCommandHandler : IRequest<AddSessionCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public AddSessionCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task Handle(AddSessionCommand request, CancellationToken cancellationToken)
    {
        await _teacherRepository.AddSessionAsync(request.ClassId, request.SessionTime);
    }
}