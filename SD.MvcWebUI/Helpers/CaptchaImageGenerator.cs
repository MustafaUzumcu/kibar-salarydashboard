using System;
using System.Drawing;
using System.IO;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.Helpers
{
    public static class CaptchaImageGenerator
    {
        public static CaptchaImageModel Generate()
        {
            var captchaImageModel = new CaptchaImageModel();

            var captchaData = DateTime.Now.Ticks.ToString("X");
            captchaData = captchaData.Substring(captchaData.Length / 2, captchaData.Length / 2);
            captchaImageModel.GeneratedCode = captchaData;

            using (var bitmap = new Bitmap(80, 20))
            {
                using (var graphic = Graphics.FromImage(bitmap))
                {
                    using (var font = new Font("Arial", 13, FontStyle.Regular))
                    {
                        using (var solidBrush = new SolidBrush(Color.Red))
                        {
                            graphic.DrawString(captchaData, font, solidBrush, 0, 0);
                            font.Dispose();
                            solidBrush.Dispose();
                            graphic.Dispose();

                            using (var memoryStream = new MemoryStream())
                            {
                                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                bitmap.Dispose();
                                var imageBuffer = memoryStream.ToArray();
                                memoryStream.Flush();
                                memoryStream.Dispose();
                                captchaImageModel.Image = imageBuffer;
                                captchaImageModel.ImageExtension = "image/png";
                                return captchaImageModel;
                            }
                        }
                    }
                }
            }

        }
    }
}
