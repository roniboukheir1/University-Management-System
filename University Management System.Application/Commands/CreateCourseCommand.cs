namespace University_Management_System.Application.Commands;

public class CreateCourseCommand
{
    public string Title { get; set; }
    public int Credits { get; set; }
    public int MaxStudents { get; set; }
    public DateTime StartOfRegistration { get; set; }
    public DateTime EndOfRegistration { get; set; }
}