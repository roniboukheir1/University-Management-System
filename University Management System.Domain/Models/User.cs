using System;
using System.Collections.Generic;
using University_Management_System.Domain.Models;
using University_Management_System.Domain.Models;

namespace University_Management_System.Domain.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long RoleId { get; set; }
    
    public string Email { get; set; } = null!;

    public virtual ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Class> TeacherPerCourses { get; set; } = new List<Class>();
}
