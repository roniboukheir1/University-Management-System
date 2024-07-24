namespace University_Management_System.Domain.Models
{
    public class Student : User
    {
        public double AverageGrade { get; set; }
        public bool CanApplyToFrance { get; set; }
        public User User { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }
    }
}