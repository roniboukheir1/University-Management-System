using MediatR;
using University_Management_System.Application.Queries.TeacherQuery;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.TeacherHandlers
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, Teacher>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherByIdQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Teacher> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            return await _teacherRepository.GetByIdAsync(request.TeacherId);
        }
    }
}