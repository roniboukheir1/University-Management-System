using System;
using System.Collections.Generic;

namespace University_Management_System.Persistence.Models;

public partial class Teacher
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Department { get; set; } = null!;

    public virtual ICollection<Course> Classes { get; set; } = new List<Course>();
}
