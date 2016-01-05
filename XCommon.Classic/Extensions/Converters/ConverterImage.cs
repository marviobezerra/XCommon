using System.Drawing;
using System.IO;

namespace XCommon.Extensions.Converters
{
    public static class ConverterImage
    {
        public static byte[] ToByte(this Image input)
        {
            MemoryStream ms = new MemoryStream();
            input.Save(ms, input.RawFormat);
            return ms.ToArray();
        }

        public static byte[] ToByte(this Bitmap bitmap)
        {
            return (bitmap as Image).ToByte();
        }

        public static Image ToImage(this byte[] input)
        {
            using (var ms = new MemoryStream(input))
            {
                return Image.FromStream(ms);
            }
        }

        public static Image ToImage(this Stream input)
        {
            return Image.FromStream(input);
        }
    }
}
