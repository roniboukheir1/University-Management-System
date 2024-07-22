using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Models;

namespace University_Management_System.Persistence.Repositories;

public class CourseRepository: ICourseRepository
{
    private readonly PostgresContext _context;

    public CourseRepository(PostgresContext context)
    {
        _context = context;
    }
    public Course GetById(long id)
    {
        if (id == null)
        {
            throw new ArgumentNullException();
        }

        var result = _context.Courses.SingleOrDefault(c => c.Id == id);
        if (result == null)
        {
            throw new NotFoundException();
        }
        return result;
    }

    public IEnumerable<Course> GetAll()
    {
        var courses = _context.Courses;
        if (courses == null)
        {
            throw new NullReferenceException();
        }
        return courses;
    }

    public void Add(Course course)
    {
        _context.Add(course);
        _context.SaveChanges();
    }

    public void Update(Course course)
    {
        var item = _context.Courses.SingleOrDefault(c => course.Id == c.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        _context.Update(course);
        _context.SaveChanges();
    }

    public void Remove(long id)
    {
        var result = _context.Courses.SingleOrDefault(c => c.Id == id);
        if (result ==null)
        {
            throw new NotFoundException();
        }
        _context.Courses.Remove(result);
        _context.SaveChanges();
    }
}
