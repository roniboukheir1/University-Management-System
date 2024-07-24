using Microsoft.EntityFrameworkCore;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;

namespace University_Management_System.Common.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    protected readonly UmsContext _context;

    public Repository(UmsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException();
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}