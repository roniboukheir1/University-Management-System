using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using University_Management_System.Common.Exceptions;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Persistence.Services;

public class TeacherServices
{
    
    private readonly ITeacherRepository _courseRepo;
    private readonly IStudentRepository _studentRepository;
    private readonly IWebHostEnvironment _env;

    public TeacherServices(ITeacherRepository courseRepo,IWebHostEnvironment env, IStudentRepository studentRepository)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _courseRepo = courseRepo;
        _studentRepository = studentRepository;
    }

    public void registerTeacher(TimeSlotModel model)
    {
        _courseRepo.registerTeacher(model);
    }

    public void SetGrade(long studentId, long courseId, double grade)
    {
        var student = _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            throw new NotFoundException("Student not found");
        }

        var enrollment = student.Enrollments.SingleOrDefault(e => e.ClassId == courseId);
        if (enrollment == null)
        {
            throw new NotFoundException("Enrollment not found");
        }

        enrollment.Grade = grade;
        _studentRepository.UpdateEnrollment(enrollment);

        student.AverageGrade = student.Enrollments.Where(e => e.Grade.HasValue).Average(e => e.Grade.Value);

        student.CanApplyToFrance = student.AverageGrade > 15;

        _studentRepository.UpdateStudent(student);
    }
    /*public void UploadProfilePicture(long studentId, IFormFile profilePicture)
    {
        if (profilePicture == null || profilePicture.Length == 0)
        {
            throw new ArgumentException("Profile picture is invalid");
        }

        var student = _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            throw new NotFoundException("Student not found");
        }

        var fileName = $"{studentId}_{Path.GetExtension(profilePicture.FileName)}";
        var filePath = Path.Combine(_env.WebRootPath, "profile_pictures", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            profilePicture.CopyTo(stream);
        }
        student.ProfilePicture = fileName;
        _studentRepository.UpdateStudent(student);
    }*/
}