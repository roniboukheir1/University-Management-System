using System;
using System.Collections.Generic;

namespace University_Management_System.Domain.Models;

public partial class SessionTime
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public long Id { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<Session> TeacherPerCoursePerSessionTimes { get; set; } = new List<Session>();
}
