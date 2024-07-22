using System;
using System.Collections.Generic;

namespace University_Management_System.Persistence.Models;

public partial class Student
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
