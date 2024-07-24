using University_Management_System.Domain.Models;

namespace University_Management_System.Persistence.Repositories;

public interface ITeacherRepository
{
    Task<Teacher> GetTeacherByIdAsync(long teacherId);
    Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    Task AddCourseAsync(Course course);
    Task UpdateTeacherAsync(Teacher teacher);
}