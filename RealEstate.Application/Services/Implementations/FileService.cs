using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace RealEstate.Application.Services.Implementations
{
    public class FileService
    {
        private readonly string _uploadPath;
        private readonly string _baseUrl;

        public FileService(IConfiguration config)   
        {
            _uploadPath = config["FileStorage:PropertyImagePath"];
            _baseUrl = config["FileStorage:BaseUrl"];
        }

        public async Task<(string fileName, string url)> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(ext))
                throw new Exception("Invalid file type.");

            if (file.Length > 5 * 1024 * 1024)
                throw new Exception("File too large.");

            var fileName = Guid.NewGuid() + ext;
            var fullPath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{_baseUrl}/{fileName}";

            return (fileName, url);
        }

        public Task<(Stream stream, string contentType)> GetImageAsync(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            var fullPath = Path.Combine(_uploadPath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException();

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fullPath, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

            return Task.FromResult<(Stream, string)>((stream, contentType));
        }
    }
}
