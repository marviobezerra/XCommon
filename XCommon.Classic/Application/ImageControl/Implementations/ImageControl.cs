using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using XCommon.Extensions.Converters;
using System;

namespace XCommon.Application.ImageControl.Implementations
{
    public class ImageControl : IImageControl
    {
        public Image ResizeImage(Image image, int maxWidth, int maxHeight, int quality)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            Bitmap newImageBitMap = new Bitmap(image, newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(newImageBitMap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            using (EncoderParameters encoderParameters = new EncoderParameters(1))
            {
                EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
                encoderParameters.Param[0] = encoderParameter;
                MemoryStream newImageStream = new MemoryStream();
                newImageBitMap.Save(newImageStream, imageCodecInfo, encoderParameters);

                return Image.FromStream(newImageStream);
            }
        }

        public byte[] ResizeImage(byte[] image, int maxWidth, int maxHeight, int quality)
        {
            return ResizeImage(image.ToImage(), maxWidth, maxHeight, quality).ToByte();
        }

        public Stream ResizeImage(Stream image, int maxWidth, int maxHeight, int quality)
        {
            return ResizeImage(image.ToByte(), maxWidth, maxHeight, quality).ToStream();
        }

        public byte[] CropImage(byte[] content, int x, int y, int width, int height)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (Image originalImage = Image.FromStream(stream))
                {
                    using (Bitmap bmp = new Bitmap(width, height))
                    {
                        bmp.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
                        using (Graphics graphic = Graphics.FromImage(bmp))
                        {
                            graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphic.DrawImage(originalImage, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, originalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
        }

        public Stream CropImage(Stream content, int x, int y, int width, int height)
        {
            return CropImage(content.ToByte(), x, y, width, height).ToStream();
        }

        public Image CropImage(Image content, int x, int y, int width, int height)
        {
            return CropImage(content.ToByte(), x, y, width, height).ToImage();
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public ImageProperty GetImageProperty(Image image)
        {
            return new ImageProperty
            {
                Height = image.Size.Height,
                Width = image.Size.Width,
                VerticalResolution = image.VerticalResolution,
                HorizontalResolution = image.HorizontalResolution
            };
        }

        public ImageProperty GetImageProperty(byte[] image)
        {
            return GetImageProperty(image.ToImage());
        }

        public ImageProperty GetImageProperty(Stream image)
        {
            return GetImageProperty(image.ToImage());
        }
    }
}
