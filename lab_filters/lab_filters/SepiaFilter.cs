using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class SepiaFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = sourceColor.R * 0.36 + sourceColor.G * 0.53 + sourceColor.B * 0.11;
            double k = 10;
            Color resultColor = Color.FromArgb(Clamp((int)(intensity + 2 * k),0,255), Clamp((int)(intensity + 0.5 * k),0,255), Clamp((int)(intensity - k),0,255));

            return resultColor;                
        }
    }
}
