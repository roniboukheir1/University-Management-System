namespace University_Management_System.Domain.Models;

public class Student : User
{
    public double AverageGrade { get; set; }
    public bool CanApplyToFrance { get; set; }
    private List<double> Grades { get; set; } = new List<double>();

    public void AddGrade(double newGrade)
    {
        Grades.Add(newGrade);
        UpdateAverageGrade();
    }

    private void UpdateAverageGrade()
    {
        AverageGrade = Grades.Average();
        CanApplyToFrance = AverageGrade > 15;
    }
}