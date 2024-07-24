using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Persistence.Repositories;

public class ClassRepository : Repository<Class> , IClassRepository
{
    private readonly UmsContext _context;
    public ClassRepository(UmsContext context) : base(context)
    {
        _context = context;
    }
}