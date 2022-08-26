using Microsoft.AspNetCore.Http;
using StudentEnrollment.Api.DTOs.Student;
using System.IO;
using System.Security.Policy;

namespace StudentEnrollment.Api.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }

        public string UploadStudentFile(byte[] file, string imageName)
        {
            if(file == null)
            {
                return string.Empty; // be path to a placeholder image
            }
            
            var folderPath = "studentpictures";
            var url = _httpContextAccessor.HttpContext?.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";

            var path = $"{_webHostEnvironment.WebRootPath}\\{folderPath}\\{fileName}";
            UploadImage(file, path);
            return $"https://{url}/{folderPath}/{fileName}";
        }

        private void UploadImage(byte[] fileBytes, string filePath)
        {
            FileInfo file = new(filePath);
            file?.Directory?.Create(); // If the directory already exists, this method does nothing.

            var fileStream = file?.Create();
            fileStream?.Write(fileBytes, 0, fileBytes.Length);
            fileStream?.Close();
        }
    }
}
