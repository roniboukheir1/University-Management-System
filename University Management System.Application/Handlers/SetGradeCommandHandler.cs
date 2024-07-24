using MediatR;
using University_Management_System.Application.Commands;
using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers;

public class SetGradeCommandHandler : IRequestHandler<SetGradeCommand>
{

    private readonly ITeacherRepository _teacherRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IStudentRepository _studentRepo;
    
    public SetGradeCommandHandler(ITeacherRepository teacherRepo, IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _teacherRepo = teacherRepo;
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }
    public async Task Handle(SetGradeCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepo.GetStudentById(request.StudentId);
        if (student == null)
            throw new NotFoundException("Student Not Found");

        var course = await _courseRepo.GetById(request.CourseId);
        if (course == null)
            throw new NotFoundException("Course Not Found");

        _teacherRepo.SetGrade(request.StudentId, request.CourseId, request.Grade);

        student.UpdateAverageGrade(request.Grade);
        await _studentRepo.Update(student);

    }
}