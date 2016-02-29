using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace lab_filters
{
    class GrayScaleFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb((int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B), (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B), (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B));

            return resultColor;
        }
    }
}
