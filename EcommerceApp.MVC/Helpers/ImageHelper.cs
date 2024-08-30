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
            string fullImagePath = Path.Combine(wwwRootPath, imagePath); // @"\images\product"
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName); // set the filename to a random new guid

            // save file
            using (var stream = new FileStream(Path.Combine(fullImagePath, fileName), FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            };

            string imageUrl = "\\" + imagePath + fileName; // need to add forward slash to image url so it is saved that way in db

            return imageUrl;
        }
    }
}
