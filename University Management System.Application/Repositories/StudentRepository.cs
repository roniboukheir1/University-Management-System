using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Exceptions;
using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Repositories;

public class StudentRepository : Repository<Student>,IStudentRepository
{
    private readonly UmsContext _context;
    private readonly IMemoryCache _cache;
    private const string UserCacheKey = "UserCache";
    private const string StudentCacheKey = "StudentCache";

    public StudentRepository(UmsContext context, IMemoryCache cache) : base(context, cache)
    {
        _context = context ;
        _cache = cache ;
    }
    
    public async Task<bool> IsEnrolledAsync(long studentId, long courseId)
    {
        return await _context.ClassEnrollments
            .AnyAsync(e => e.StudentId == studentId && e.ClassId == courseId);
    }
    public override async Task<Student> GetByIdAsync(long studentId)
    {
        string cacheKey = $"{UserCacheKey}_{studentId}";
        var user =  _cache.Get<User>(cacheKey);
        if (user == null)
        {
            user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == studentId && u.RoleId == Role.Student);
            if (user != null)
            {
                _cache.Set(cacheKey, user, TimeSpan.FromMinutes(30));
            }
        }
        return (Student) user;
    }
    public async Task AddEnrollmentAsync(ClassEnrollment enrollment)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == enrollment.ClassId);

        if (course == null)
        {
            throw new NotFoundException("Course not found.");
        }

        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (!course.EnrolmentDateRange.HasValue || 
            !course.EnrolmentDateRange.Value.Contains(currentDate))
        {
            throw new InvalidOperationException("Enrollment is not allowed outside the allowed date range.");
        }

        var enrolledCount = await _context.ClassEnrollments
            .CountAsync(e => e.ClassId == enrollment.ClassId);

        if (course.MaxStudentsNumber.HasValue && enrolledCount >= course.MaxStudentsNumber.Value)
        {
            throw new InvalidOperationException("The maximum number of students for this course has been reached.");
        }

        await _context.ClassEnrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();

        _cache.Remove(StudentCacheKey);
    }


    public override async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.RoleId == Role.Student)
            .Select(u => (Student)u)
            .ToListAsync();
    }
}
