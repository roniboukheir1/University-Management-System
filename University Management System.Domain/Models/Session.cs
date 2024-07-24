using System;
using System.Collections.Generic;

namespace University_Management_System.Domain.Models;

public partial class Session
{
    public long Id { get; set; }

    public long TeacherPerCourseId { get; set; }

    public long SessionTimeId { get; set; }

    public virtual SessionTime SessionTime { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;
}
