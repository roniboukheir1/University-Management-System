using University_Management_System.Persistence.Models;

namespace University_Management_System.Persistence.Repositories;

public interface IStudentRepository
{
    public Student GetStudentById(long studentId);
    public void AddEnrollment(Enrollment enrollment);
    public bool IsEnrolled(long studentId, long courseId);
    public IEnumerable<Student> GetAll();
}