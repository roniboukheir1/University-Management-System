using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace University_Management_System.Domain.Models;

public partial class Course
{
    public long CourseId { get; set; }

    public string? Name { get; set; }

    public int? MaxStudentsNumber { get; set; }

    public NpgsqlRange<DateOnly>? EnrolmentDateRange { get; set; }

    public virtual ICollection<Class> TeacherPerCourses { get; set; } = new List<Class>();
}
