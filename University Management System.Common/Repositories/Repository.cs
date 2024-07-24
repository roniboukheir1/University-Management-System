using AspNetCore.CacheOutput;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Common.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    protected readonly UmsContext _context;
    protected readonly IMemoryCache _cache;
    private readonly string CacheKeyPrefix = $"{typeof(T).Name}";

    public Repository(UmsContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        string cacheKey = $"{CacheKeyPrefix}All";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<T> entities))
        {
            entities = await _context.Set<T>().ToListAsync();
            _cache.Set(cacheKey, entities, TimeSpan.FromMinutes(30));
        }
        return entities;
    }

    public virtual async Task<T> GetByIdAsync(long id)
    {
        string cacheKey = $"{CacheKeyPrefix}_{id}";
        if (!_cache.TryGetValue(cacheKey, out T entity))
        {
            entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _cache.Set(cacheKey, entity, TimeSpan.FromMinutes(30));
            }
        }
        return entity;  
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
     //   InvalidCache();
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