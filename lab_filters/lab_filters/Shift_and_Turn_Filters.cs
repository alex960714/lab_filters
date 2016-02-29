using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class ShiftFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k, l;
            k = Clamp(x + 50, 0, sourceImage.Width - 1);
            l = Clamp(y, 0, sourceImage.Height - 1);
            Color sourceColor = sourceImage.GetPixel(k, l);
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);

            return resultColor;
        }
    }

    class TurnFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k, l;
            int x0 = (int)((sourceImage.Width - 1) / 2);
            int y0 = (int)((sourceImage.Height - 1) / 2);
            k = Clamp((int)((x - x0) / Math.Sqrt(2) - (y - y0) / Math.Sqrt(2) + x0), 0, sourceImage.Width - 1);
            l = Clamp((int)((x - x0) / Math.Sqrt(2) + (y - y0) / Math.Sqrt(2) + y0), 0, sourceImage.Height - 1);
            Color sourceColor = sourceImage.GetPixel(k, l);
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);

            return resultColor;
        }
    }
}
