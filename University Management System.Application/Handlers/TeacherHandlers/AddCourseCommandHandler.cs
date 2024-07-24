using MediatR;
using University_Management_System.Application.Commands.TeacherCommand;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.TeacherHandlers;

public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ICourseRepository _courseRepository;

    public AddCourseCommandHandler(ITeacherRepository teacherRepository, ICourseRepository course)
    {
        _teacherRepository = teacherRepository;
        _courseRepository = course;
    }

    public async Task Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        await _teacherRepository.AddCourseAsync(course, request.TeacherId);
    }
}