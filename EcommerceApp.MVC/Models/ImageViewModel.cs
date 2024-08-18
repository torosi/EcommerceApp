namespace EcommerceApp.MVC.Models
{
    public class ImageViewModel
    {
        public byte[]? ImageData { get; set; }
        // going to use imagebase64 to display the image
        public string? ImageBase64 => ImageData != null ? Convert.ToBase64String(ImageData) : null;
    }
}
