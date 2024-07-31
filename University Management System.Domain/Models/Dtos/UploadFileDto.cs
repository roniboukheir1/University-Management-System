using Microsoft.AspNetCore.Http;

namespace University_Management_System.Domain.Models.Dtos;

public class UploadFileDto
{
    public IFormFile file { get; set; }
    public string containerName { get; set; }
    public string blobName { get; set; }
}