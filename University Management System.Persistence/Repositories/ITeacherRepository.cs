using University_Management_System.Persistence.Models;

namespace University_Management_System.Persistence.Repositories;

public interface ITeacherRepository
{
    public void registerTeacher(TimeSlotModel model);
}