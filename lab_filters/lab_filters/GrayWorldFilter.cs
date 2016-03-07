using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class GrayWorldFilter
    {
        protected Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int avg,int r,int g,int b)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(Clamp((int)(sourceColor.R * avg / r), 0, 255), Clamp((int)(sourceColor.G * avg / g), 0, 255), Clamp((int)(sourceColor.B * avg / b), 0, 255));

            return resultColor;
        }

        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int r = 0, g = 0, b = 0;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    r += sourceColor.R;
                    g += sourceColor.G;
                    b += sourceColor.B;
                }
            }
            r *= (int)(r / (sourceImage.Width * sourceImage.Height));
            g *= (int)(g / (sourceImage.Width * sourceImage.Height));
            b *= (int)(b / (sourceImage.Width * sourceImage.Height));
            int avg = (r + g + b) / 3;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j,avg,r,g,b));
                }
            }

            return resultImage;
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}
