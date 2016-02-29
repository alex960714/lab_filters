using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class GlassFilter : Filters
    {
        private Random rand = new Random();
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k, l;
            k = Clamp((int)(x + (rand.NextDouble() - 0.5) * 10), 0, sourceImage.Width - 1);
            l = Clamp((int)(y + (rand.NextDouble() - 0.5) * 10), 0, sourceImage.Height - 1);
            Color sourceColor = sourceImage.GetPixel(k, l);
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);

            return resultColor;
        }
    }
}
