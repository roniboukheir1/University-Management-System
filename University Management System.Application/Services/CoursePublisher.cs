using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using University_Management_System.Domain.Models;
using University_Management_System.Domain.Models.Request;
using IModel = RabbitMQ.Client.IModel;

namespace University_Management_System.Application.Services;

public class CoursePublisher
{
    private readonly IModel _channel;

    public CoursePublisher(IModel channel)
    {
        _channel = channel;
    }

    public void PublicCourse(Course course)
    {
        var message = JsonSerializer.Serialize(course);
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(
            exchange: "course_exchange",
            routingKey: "course_routing_key",
            basicProperties: null,
            body: body);            
    }

    public void PublishEnrollment(ClassEnrollment enrollmentRequest)
    {
        var message = JsonSerializer.Serialize(enrollmentRequest);
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(
            exchange: "Enrollment",
            routingKey: "Enrollment_routing",
            basicProperties:null,
            body: body
                );
    }
}