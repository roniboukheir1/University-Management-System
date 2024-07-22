using Microsoft.EntityFrameworkCore;
using University_Management_System.Persistence.Models;
using Microsoft.Extensions.Caching.Memory;

namespace University_Management_System.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly PostgresContext _context;
    private readonly IMemoryCache _cache;

    public StudentRepository(PostgresContext context, IMemoryCache cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public Student GetStudentById(long studentId)
    {
        string cacheKey = $"Student_{studentId}";
        if (!_cache.TryGetValue(cacheKey, out Student student))
        {
            student = _context.Students.Include(s => s.Enrollments).SingleOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                _cache.Set(cacheKey, student, TimeSpan.FromMinutes(30));
            }
        }
        return student;
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
        _cache.Remove($"Student_{enrollment.StudentId}");
        _cache.Remove($"Course_{enrollment.ClassId}");
    }

    public bool IsEnrolled(long studentId, long courseId)
    {
        string cacheKey = $"IsEnrolled_{studentId}_{courseId}";
        if (!_cache.TryGetValue(cacheKey, out bool isEnrolled))
        {
            isEnrolled = _context.Enrollments.Any(e => e.StudentId == studentId && e.ClassId == courseId);
            _cache.Set(cacheKey, isEnrolled, TimeSpan.FromMinutes(30));
        }
        return isEnrolled;
    }

    public IEnumerable<Student> GetAll()
    {
        const string cacheKey = "AllStudents";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Student> students))
        {
            students = _context.Students.ToList();
            _cache.Set(cacheKey, students, TimeSpan.FromMinutes(30));
        }
        return students;
    }
    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }
    public void UpdateEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        _context.SaveChanges();
    }

}
