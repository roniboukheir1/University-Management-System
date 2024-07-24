using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Persistence.Repositories;

public class ClassRepository : Repository<Class> , IClassRepository
{
    private readonly UmsContext _context;
    private readonly IMemoryCache _cache;
    public ClassRepository(UmsContext context, IMemoryCache cache) : base(context, cache)
    {
        _context = context;
        _cache = cache;
    }
}