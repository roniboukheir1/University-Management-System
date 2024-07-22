using University_Management_System.Persistence.Models.enums;

namespace University_Management_System.Persistence.Models;

public class TimeSlotModel
{
    public long Id { get; set; }
    public DayPattern DayPattern { get; set; } // TR or MWF
    public String StartTime { get; set; } 
    public String EndTime { get; set; } 
    public long CourseId { get; set; } 
   // public Course Course { get; set; } 

}