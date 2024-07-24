using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Persistence.Repositories;

public class StudentRepository : Repository<User>,IStudentRepository
{
    private readonly UmsContext _context;
    private readonly IMemoryCache _cache;
    private const string UserCacheKey = "UserCache";
    private const string StudentCacheKey = "StudentCache";

    public StudentRepository(UmsContext context, IMemoryCache cache) : base(context)
    {
        _context = context ;
        _cache = cache ;
    }
    
    public async Task UpdateStudentAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        _cache.Remove($"{UserCacheKey}_{user.Id}");
        _cache.Remove(UserCacheKey);
    }

    public async Task AddStudentAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        _cache.Remove(UserCacheKey);
    }

    public async Task<bool> IsEnrolledAsync(long studentId, long courseId)
    {
        return await _context.ClassEnrollments
            .AnyAsync(e => e.StudentId == studentId && e.ClassId == courseId);
    }
    public async Task<Student> GetStudentByIdAsync(long studentId)
    {
        string cacheKey = $"{UserCacheKey}_{studentId}";
        var user =  _cache.Get<User>(cacheKey);
        if (user == null)
        {
            user = await _context.Users.FindAsync(studentId);
            if (user != null)
            {
                _cache.Set(cacheKey, user, TimeSpan.FromMinutes(30));
            }
        }
        return (Student) user;
    }

    public Task AddEnrollmentAsync(ClassEnrollment enrollment)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        var user = _context.Users.FindAsync(student.Id);
        if (user != null)
        {
            _context.Entry(user).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
            _cache.Remove($"{StudentCacheKey}_{student.Id}");
        }
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _context.Users
            .Where(u => u.RoleId == Role.Student)
            .Select(u => (Student)u)
            .ToListAsync();
    }
}
