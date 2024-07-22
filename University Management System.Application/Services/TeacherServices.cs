using University_Management_System.Persistence.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Persistence.Services;

public class TeacherServices
{
    
    private readonly ITeacherRepository _courseRepo;

    public TeacherServices(ITeacherRepository courseRepo)
    {
        _courseRepo = courseRepo;
    }

    public void registerTeacher(TimeSlotModel model)
    {
        _courseRepo.registerTeacher(model);
    }
}