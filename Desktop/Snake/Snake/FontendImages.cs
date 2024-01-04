using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
    public static class FontendImages
    {
        public static readonly ImageSource Empty = loadImage("Empty.png");
        public static readonly ImageSource Body = loadImage("Body.png");
        public static readonly ImageSource Head = loadImage("Head.png");
        public static readonly ImageSource Food = loadImage("Food.png");
        public static readonly ImageSource DeadBody = loadImage("DeadBody.png");
        public static readonly ImageSource DeadHead = loadImage("DeadHead.png");

        public static ImageSource loadImage(string fileName)
        {
            return new BitmapImage(new Uri($"SnakeAssets/{fileName}", UriKind.Relative));
        }
    }
}
