using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University_Management_System.Application.Services;
using University_Management_System.Domain.Models.Dtos;

namespace University_Management_System.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public FilesController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    [Authorize(Roles = "Admin,Student,Teacher")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileDto request)
    {
        if (request.file == null)
        {
            return BadRequest("File is empty or not selected");
        }

        await _fileStorageService.UploadFileAsync(request.file, request.containerName, request.blobName);
        return Ok("File Uploaded Successfully");
    }

    [HttpGet("Downlaod")]
    [Authorize(Roles = "Admin,Student,Teacher")]
    public async Task<IActionResult> DownloadFile([FromQuery] string containerName, [FromQuery] string blobName)
    {
        byte[] fileBytes = await _fileStorageService.DownloadFileAsync(containerName, blobName);
        return File(fileBytes, "application/octet-stream", blobName);
    }
    
}