using System;
using System.Collections.Generic;

namespace University_Management_System.Persistence.Models;
public class Student
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public double AverageGrade { get; set; }
    public bool CanApplyToFrance { get; set; }
    public string? ProfilePicture { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

