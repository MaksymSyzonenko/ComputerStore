using ComputerStore.Models;
using Microsoft.AspNetCore.Hosting;

namespace ComputerStore.Services
{
    public static class ImageService
    {
        public static string DownloadImage(IFormFile imageUpload)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageUpload.CopyTo(stream);
            }
            return fileName;
        }
        public static string UpdateImage(string existingImage, IFormFile imageUpload)
        {
            if (!string.IsNullOrEmpty(existingImage))
            {
                string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingImage.TrimStart('/'));
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);

            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            imageUpload.CopyTo(stream);
            return fileName;
        }
        public static void DeleteImage(string productImage, IWebHostEnvironment webHostEnvironment)
        {
            if (!string.IsNullOrEmpty(productImage))
            {
                string imagePath = Path.Combine(webHostEnvironment.WebRootPath, productImage.TrimStart('/'));
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
        }
    }
}
