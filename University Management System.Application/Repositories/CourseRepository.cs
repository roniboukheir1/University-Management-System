using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Repositories;
using University_Management_System.Infrastructure;

namespace University_Management_System.Persistence.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{

    private readonly IMemoryCache _cache;
    private readonly UmsContext _context;

    public CourseRepository(UmsContext context, IMemoryCache cache) : base(context, cache)
    {
        _context = context;
        _cache = cache;
    }
}
