using System;
using System.Collections.Generic;

namespace University_Management_System.Domain.Models;

public partial class Role
{
    public static int Admin = 1;
    public static int Teacher = 2;
    public static int Student = 3;
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
