using System;
using System.Runtime.Serialization;

namespace XCommon.Application.ImageControl
{
    public class ImageCrop
    {
        public int PointX { get; set; }
        
        public int PointY { get; set; }
        
        public int Width { get; set; }
        
        public int Height { get; set; }
    }
}
