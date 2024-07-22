using Microsoft.Extensions.Logging;
using University_Management_System.Common.Exceptions;
using University_Management_System.Persistence.Models;

namespace University_Management_System.Persistence.Repositories;

public class TeacherRepository : ITeacherRepository
{
        
    private readonly PostgresContext _context;
    private readonly ILogger<TeacherRepository> _logger;

    public TeacherRepository(PostgresContext context, ILogger<TeacherRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public void registerTeacher(TimeSlotModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _logger.LogInformation("Registering course with TimeSlotModel: {@model}", model);

        var course = _context.Courses.SingleOrDefault(c => c.Id == model.CourseId);
        if (course == null)
        {
            _logger.LogWarning("Course not found: {CourseId}", model.CourseId);
            throw new NotFoundException("Course not found");
        }

        if (!TimeSpan.TryParse(model.StartTime, out var startTime))
        {
            _logger.LogError("Invalid StartTime format: {StartTime}", model.StartTime);
            throw new FormatException("Invalid StartTime format");
        }

        if (!TimeSpan.TryParse(model.EndTime, out var endTime))
        {
            _logger.LogError("Invalid EndTime format: {EndTime}", model.EndTime);
            throw new FormatException("Invalid EndTime format");
        }

        var timeslot = new TimeSlotModel
        {
            DayPattern = model.DayPattern,
            StartTime = startTime.ToString(),
            EndTime = endTime.ToString(),
            CourseId = model.CourseId,
        };
        
        course.TimeSlots.Add(timeslot);
        _context.Courses.Update(course);
        _context.TimeSlots.Add(timeslot); // do we really need to save this in the database?
        _context.SaveChanges();
        _logger.LogInformation("Course registered with time slot successfully: {@timeSlot}", timeslot);
    }
}