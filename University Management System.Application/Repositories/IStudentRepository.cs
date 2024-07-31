using University_Management_System.Common.Repositories;
using University_Management_System.Domain.Models;

namespace University_Management_System.Application.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    public Task AddEnrollmentAsync(ClassEnrollment enrollment);
    public Task<bool> IsEnrolledAsync(long studentId, long courseId);
}