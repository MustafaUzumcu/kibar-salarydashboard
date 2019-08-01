namespace SD.MvcWebUI.Models
{
    public class CaptchaImageModel
    {
        public string GeneratedCode { get; set; }

        public byte[] Image { get; set; }

        public string ImageExtension { get; set; } = "image/png";
    }
}
