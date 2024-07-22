using Microsoft.EntityFrameworkCore;
using University_Management_System.Persistence.Models;

namespace University_Management_System.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly PostgresContext _context;

    public StudentRepository(PostgresContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Student GetStudentById(long studentId)
    {
        return _context.Students.Include(s => s.Enrollments).SingleOrDefault(s => s.Id == studentId);
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
    }

    public bool IsEnrolled(long studentId, long courseId)
    {
        return _context.Enrollments.Any(e => e.StudentId == studentId && e.ClassId == courseId);
    }

    public IEnumerable<Student> GetAll()
    {
        return _context.Students;
    }
}
