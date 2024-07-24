using MediatR;
using University_Management_System.Application.Queries.TeacherQuery;
using University_Management_System.Domain.Models;
using University_Management_System.Persistence.Repositories;

namespace University_Management_System.Application.Handlers.TeacherHandlers
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, IEnumerable<Teacher>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetAllTeachersQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _teacherRepository.GetAllAsync();
        }
    }
}