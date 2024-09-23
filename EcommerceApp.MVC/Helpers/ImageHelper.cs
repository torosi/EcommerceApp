using Microsoft.AspNetCore.Hosting;

namespace EcommerceApp.MVC.Helpers
{
    public class ImageHelper
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<string> UploadImageAsync(IFormFile imageFile, string imagePath = @"images")
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fullImagePath = Path.Combine(wwwRootPath, "images", imagePath); // @"\images\product"
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName); // set the filename to a random new guid

            // save file
            using (var stream = new FileStream(Path.Combine(fullImagePath, fileName), FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            };

            string imageUrl = "\\images\\" + imagePath + "\\" + fileName; // need to add forward slash to image url so it is saved that way in db

            return imageUrl;
        }

        public bool DeleteImage(string imageUrl)
        {
            // Check if the imageUrl is null or empty
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return false; // Invalid input
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string finalPath = Path.Combine(wwwRootPath, imageUrl.TrimStart('\\'));

            // Normalize the path to ensure it's absolute and safe
            finalPath = Path.GetFullPath(finalPath);

            // Check if the file exists
            if (System.IO.File.Exists(finalPath))
            {
                System.IO.File.Delete(finalPath); // Delete the file
                return true;
            }

            return false;
        }

        /// <summary>
        /// This is a method to check that a file is an image. We do not want to allow images to be uploaded that are not a correct image file type.
        /// </summary>
        /// <param name="file">IFormFile file type to be validated</param>
        /// <returns></returns>
        public bool IsImageFile(IFormFile file)
        {
            // List of allowed image content types
            var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };
            // List of allowed file extensions
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

            // Check the file's content type
            if (!allowedContentTypes.Contains(file.ContentType.ToLower()))
            {
                return false;
            }

            // Check the file's extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return false;
            }

            return true;
        }
    }
}
