using System.Drawing;
using System.IO;

namespace XCommon.Application.ImageControl
{
    public interface IImageControl
    {
        ImageProperty GetImageProperty(Image image);
        ImageProperty GetImageProperty(byte[] image);
        ImageProperty GetImageProperty(Stream image);
        Image ResizeImage(Image image, int maxWidth, int maxHeight, int quality);
        byte[] ResizeImage(byte[] image, int maxWidth, int maxHeight, int quality);
        Stream ResizeImage(Stream image, int maxWidth, int maxHeight, int quality);
        Image CropImage(Image content, int x, int y, int width, int height);
        byte[] CropImage(byte[] content, int x, int y, int width, int height);
        Stream CropImage(Stream content, int x, int y, int width, int height);
    }
}
