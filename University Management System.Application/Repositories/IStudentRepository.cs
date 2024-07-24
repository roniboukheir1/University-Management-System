using University_Management_System.Domain.Models;

namespace University_Management_System.Persistence.Repositories;

public interface IStudentRepository
{
    Task<bool> IsEnrolledAsync(long studentId, long courseId);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(long studentId);
    Task AddEnrollmentAsync(ClassEnrollment enrollment);
    Task UpdateStudentAsync(Student student);
}