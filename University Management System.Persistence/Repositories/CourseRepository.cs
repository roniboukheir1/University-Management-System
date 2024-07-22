using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Models;
using Microsoft.Extensions.Caching.Memory;

namespace University_Management_System.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly PostgresContext _context;
    private readonly IMemoryCache _cache;

    public CourseRepository(PostgresContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public IQueryable<Course> Get()
    {
        const string cacheKey = "AllCourses";
        if (!_cache.TryGetValue(cacheKey, out IQueryable<Course> courses))
        {
            courses = _context.Courses.AsQueryable();
            _cache.Set(cacheKey, courses, TimeSpan.FromMinutes(30));
        }
        return courses;
    }

    public Course GetById(long id)
    {
        string cacheKey = $"Course_{id}";
        if (!_cache.TryGetValue(cacheKey, out Course course))
        {
            course = _context.Courses.SingleOrDefault(c => c.Id == id);
            if (course == null)
            {
                throw new NotFoundException();
            }
            _cache.Set(cacheKey, course, TimeSpan.FromMinutes(30));
        }
        return course;
    }

    public IEnumerable<Course> GetAll()
    {
        const string cacheKey = "AllCoursesList";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Course> courses))
        {
            courses = _context.Courses.ToList();
            if (courses == null)
            {
                throw new NullReferenceException();
            }
            _cache.Set(cacheKey, courses, TimeSpan.FromMinutes(30));
        }
        return courses;
    }

    public void Add(Course course)
    {
        _context.Add(course);
        _context.SaveChanges();
        _cache.Remove("AllCourses");
        _cache.Remove("AllCoursesList");
    }

    public void Update(Course course)
    {
        var item = _context.Courses.SingleOrDefault(c => course.Id == c.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        _context.Update(course);
        _context.SaveChanges();
        _cache.Remove($"Course_{course.Id}");
        _cache.Remove("AllCourses");
        _cache.Remove("AllCoursesList");
    }

    public void Remove(long id)
    {
        var result = _context.Courses.SingleOrDefault(c => c.Id == id);
        if (result == null)
        {
            throw new NotFoundException();
        }
        _context.Courses.Remove(result);
        _context.SaveChanges();
        _cache.Remove($"Course_{id}");
        _cache.Remove("AllCourses");
        _cache.Remove("AllCoursesList");
    }
}
