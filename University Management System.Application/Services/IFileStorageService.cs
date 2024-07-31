using Microsoft.AspNetCore.Http;

namespace University_Management_System.Application.Services;

public interface IFileStorageService
{
    Task UploadFileAsync(IFormFile file, string containerName, string blobName);
    Task<byte[]> DownloadFileAsync(string containerName, string blobName);
}