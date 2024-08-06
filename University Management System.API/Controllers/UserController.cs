using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using University_Management_System.Persistence;
using Microsoft.Extensions.FileProviders;
using University_Management_System.Infrastructure;
using Microsoft.Extensions.Logging;

namespace University_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly UmsContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<UserController> _logger;

        public UserController(UmsContext context, IFileProvider fileProvider, ILogger<UserController> logger)
        {
            _context = context;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        [HttpPost("{id}/uploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(long id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("No file uploaded for user {UserId}.", id);
                return BadRequest("No file uploaded.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                return NotFound("User not found.");
            }

            var uploadsFolderPath = Path.Combine(_fileProvider.GetFileInfo("wwwroot").PhysicalPath, "profile_pictures");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = $"{id}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                user.ProfilePicture = fileName;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Profile picture for user {UserId} uploaded successfully. FilePath: {FilePath}", id, filePath);

                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading profile picture for user {UserId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading profile picture.");
            }
        }
    }
}
