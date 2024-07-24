using System;
using System.Collections.Generic;

namespace University_Management_System.Domain.Models;

public partial class Class
{
    public long Id { get; set; }

    public long TeacherId { get; set; }

    public long CourseId { get; set; }

    public virtual ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();

    public virtual Course Course { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;

    public virtual ICollection<Session> TeacherPerCoursePerSessionTimes { get; set; } = new List<Session>();
}
