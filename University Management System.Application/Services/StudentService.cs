using Microsoft.Extensions.Logging;
using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Persistence.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository, ILogger<StudentService> logger)
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public void EnrollStudent(long studentId, long courseId)
    {
        var student = _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            _logger.LogWarning("Student not found: {StudentId}", studentId);
            throw new NotFoundException("Student not found");
        }

        var course = _courseRepository.GetById(courseId);
        if (course == null)
        {
            _logger.LogWarning("Course not found: {CourseId}", courseId);
            throw new NotFoundException("Course not found");
        }

        if (DateTime.Now < course.startOfRegistration || DateTime.Now > course.endOfRegistration)
        {
            _logger.LogWarning("Cannot enroll outside of course registration date range: {CourseId}", courseId);
            throw new InvalidOperationException("Cannot enroll outside of course registration date range");
        }

        if (_studentRepository.IsEnrolled(studentId, courseId))
        {
            _logger.LogWarning("Student already enrolled in course: {StudentId}, {CourseId}", studentId, courseId);
            throw new InvalidOperationException("Student already enrolled in course");
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            ClassId = courseId,
        };

        _studentRepository.AddEnrollment(enrollment);
        _logger.LogInformation("Student enrolled successfully: {StudentId}, {CourseId}", studentId, courseId);
    }

    public IEnumerable<Student> GetAll()
    {
        return _studentRepository.GetAll();
    }
}
