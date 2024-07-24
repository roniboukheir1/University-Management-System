using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Repositories;
using University_Management_System.Infrastructure;

namespace University_Management_System.Persistence.Repositories;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{

    private readonly UmsContext _context;
    private readonly IMemoryCache _cache;
    private readonly IClassRepository _classRepository;
    private readonly string TeacherCacheKey = "TeacherCache";
    private readonly string ClassCacheKey = "ClassCache";
    private readonly string SessionCacheKey = "SessionCache";
    public TeacherRepository(UmsContext context, IMemoryCache cache, IClassRepository clsRepo) : base(context, cache)
    {
        _context = context;
        _cache = cache;
        _classRepository = clsRepo;
    }

    public async override Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.RoleId == Role.Teacher)
            .Select(u => (Teacher) u).ToListAsync();
    }

    public async Task AddCourseAsync(Course course, long teacherId)
    {
        string cacheKey = $"{TeacherCacheKey}_{teacherId}_courses";
        string courseCacheKey = $"{ClassCacheKey}_{course.CourseId}";
        string teacherCacheKey = $"{TeacherCacheKey}_{teacherId}";
        _cache.Remove(cacheKey);
        Teacher teacher = _cache.Get<Teacher>(teacherCacheKey);
        if (teacher == null)
            teacher = await GetByIdAsync(teacherId);
        
        if (teacher == null)
        {
            throw new NotFoundException("Teacher Not Found");
        }

        Course item = _cache.Get<Course>(ClassCacheKey);
        if(item == null)
            item = await _context.Courses.FirstOrDefaultAsync(u => u.CourseId == course.CourseId);
        if (item == null)
        {
            throw new NotFoundException("Course Not Found");
        }
        var @class = new Class
        {
            TeacherId = teacherId,
            CourseId = course.CourseId
        };
        await _context.TeacherPerCourses.AddAsync(@class);
        await _context.SaveChangesAsync();
        
        var courses = await _context.TeacherPerCourses
            .Where(u=> u.TeacherId == teacherId)
            .Select(u => u.Course)
            .ToListAsync();
        _cache.Set(cacheKey, courses, TimeSpan.FromMinutes(30));    
    }
    public async Task AddSessionAsync(long classId, SessionTime? sessionTime)
    {
        string CacheKey = $"{SessionCacheKey}_{classId}";
        var @class = await _classRepository.GetByIdAsync(classId);
        if (@class == null)
        {
            throw new NotFoundException("Class Not Found");
        }

        Session session = new Session
        {
            TeacherPerCourseId = classId,
            SessionTimeId = sessionTime.Id
        };
        await _context.TeacherPerCoursePerSessionTimes.AddAsync(session);
        await _context.SaveChangesAsync();
        _cache.Set(CacheKey, session, TimeSpan.FromMinutes(30));
    }
    public async override Task<Teacher> GetByIdAsync(long teacherId)
    {
        string cacheKey = $"{TeacherCacheKey}_{teacherId}";
        var teacher = _cache.Get<User>(cacheKey);
        if (teacher == null)
        {
            teacher = await _context.Users.FirstOrDefaultAsync(u => u.Id == teacherId && u.RoleId == Role.Teacher);
            if (teacher != null)
            {
                _cache.Set(cacheKey, teacher, TimeSpan.FromMinutes(30));
            }
        }
        return (Teacher)teacher;
    }

    public UmsContext GetContext()
    {
        return _context;    
    }
}