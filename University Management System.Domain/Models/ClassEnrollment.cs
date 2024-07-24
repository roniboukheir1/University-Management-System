using System;
using System.Collections.Generic;

namespace University_Management_System.Domain.Models;

public partial class ClassEnrollment
{
    public long Id { get; set; }

    public long ClassId { get; set; }

    public long StudentId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
