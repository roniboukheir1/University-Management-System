using System;
using System.Collections.Generic;

namespace University_Management_System.Persistence.Models;

public class Enrollment
{
    public long StudentId { get; set; }
    public long ClassId { get; set; }
    public long Id { get; set; }
    public double? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;
    public virtual Student Student { get; set; } = null!;
}

