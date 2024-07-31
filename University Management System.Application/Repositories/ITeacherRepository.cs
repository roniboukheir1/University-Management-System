using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;
using University_Management_System.Infrastructure;

namespace University_Management_System.Application.Repositories;

public interface ITeacherRepository : IRepository<Teacher>
{
    public Task<Teacher> GetByIdAsync(long teacherId);
    public Task<IEnumerable<Teacher>> GetAllAsync();
    public Task AddCourseAsync(Course course, long teacherId);
    public Task AddSessionAsync(long classId, SessionTime? sessionTime);
    public UmsContext GetContext();
}