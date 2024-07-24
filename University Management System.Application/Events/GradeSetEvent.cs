using MediatR;

namespace University_Management_System.Application.Events
{
    public class GradeSetEvent : INotification
    {
        public long StudentId { get; }

        public GradeSetEvent(long studentId)
        {
            StudentId = studentId;
        }
    }
}