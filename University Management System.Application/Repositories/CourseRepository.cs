using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Application.Services;
using University_Management_System.Common.Repositories;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{

    private readonly IMemoryCache _cache;
    private readonly UmsContext _context;
    private readonly CoursePublisher _publisher;
    public CourseRepository(UmsContext context, IMemoryCache cache, CoursePublisher publisher) : base(context, cache)
    {
        _context = context;
        _cache = cache;
        _publisher = publisher;
    }

    public async Task AddAsync(Course course)
    {
        _publisher.PublicCourse(course);
    }
}
