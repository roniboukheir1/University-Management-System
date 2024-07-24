/*using University_Management_System.Application.Commands;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Persistence.Services;

public class CourseService
{
    private readonly ICourseRepository _courseRepo;

    public CourseService(ICourseRepository courseRepo)
    {
        _courseRepo = courseRepo;
    }

    public Course CreateCourse(CreateCourseCommand command)
    {
        var course = new Course
        {
            Title = command.Title,
            Credits = command.Credits,
            maxStudents = command.MaxStudents,
            startOfRegistration = command.StartOfRegistration,
            endOfRegistration = command.EndOfRegistration,
            TeacherId = -1,
        };
        _courseRepo.Add(course);
        return course;
    }
    
    public Course GetById(long id)
    {
        if (id == null)
        {
            throw new ArgumentNullException();
        }
        var result = _courseRepo.GetById(id);
        if (result == null)
        {
            throw new NotFoundException();
        }
        return result;
    }

    public IEnumerable<Course> GetAll()
    {
        var courses = _courseRepo.GetAll();
        if (courses == null)
        {
            throw new NullReferenceException();
        }

        return courses;
    }
    
    public void Update(long id, CreateCourseCommand command)
    {
        var item = GetById(id);
        if (item == null)
        {
            throw new NotFoundException("Course Not Found");
        }

        item.Credits = command.Credits;
        item.Title = command.Title;
        item.endOfRegistration = command.EndOfRegistration;
        item.startOfRegistration = command.StartOfRegistration;
        item.maxStudents = command.MaxStudents;
        _courseRepo.Update(item);
    }

    public void DeleteCourse(long id)
    {
        _courseRepo.Remove(id);
    }

}*/