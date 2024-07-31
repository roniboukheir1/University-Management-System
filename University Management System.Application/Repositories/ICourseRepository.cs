using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    public Task<Course> GetByIdAsync(long id);
    public Task<IEnumerable<Course>> GetAllAsync();
    public Task AddAsync(Course course);
    public Task UpdateAsync(Course course);
    public Task DeleteAsync(long id);
}