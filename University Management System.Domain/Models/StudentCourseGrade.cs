namespace University_Management_System.Domain.Models;

public class StudentCourseGrade
{
    public long StudentCourseGradeId { get; set; }
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public double Grade { get; set; }
    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
}