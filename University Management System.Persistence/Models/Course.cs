using System;
using System.Collections.Generic;

namespace University_Management_System.Persistence.Models;

public partial class Course
{
    public long Id { get; set; }
    
    public long TeacherId { get; set; }
    public string Title { get; set; }
    public DateTime startOfRegistration { get; set; }
    public DateTime endOfRegistration { get; set; }
    
    public int maxStudents { get; set; }
    public int Credits { get; set; }

    public ICollection<TimeSlotModel> TimeSlots { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Teacher Teacher { get; set; } = null!;
}
