using University_Management_System.Domain.Models;

namespace University_Management_System.Persistence.Repositories;

public interface ICourseRepository
{
    public Course GetById(long id);
    public IEnumerable<Course> GetAll();
    public void Add(Course course);
    public void Update(Course course);
    public void Remove(long id);
}