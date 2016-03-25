using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class MathMorphology: Filters
    {
        protected bool isDilation;
        protected bool[,] kernel = null;
        protected MathMorphology() { }

        public MathMorphology(bool[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int max = 0;
            int min = int.MaxValue;
            Color clr = Color.Black;
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color sourceColor = sourceImage.GetPixel(idX, idY);
                    int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
                    if (isDilation)
                    {
                        if ((kernel[k + radiusX, l + radiusY]) && (intensity > max))
                        {
                            max = intensity;
                            clr = sourceColor;
                        }
                    }
                    else
                    {
                        if (intensity < min)
                        {
                            min = intensity;
                            clr = sourceColor;
                        }
                    }
                }

            return clr;
        }

    }

    class Dilation: MathMorphology
    {
        public Dilation()
        {
            isDilation = true;
            kernel = new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
        }
    }

    class Erosion: MathMorphology
    {
        public Erosion()
        {
            isDilation = false;
            kernel = new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
        }
    }

    class Opening : MathMorphology
    {
        public Opening()
        {
            kernel = new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            isDilation = false;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            isDilation = true;
            for (int i = 0; i < currImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                for (int j = 0; j < currImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
                }
            }
            return resultImage;
        }
    }    
    
    class Closing : MathMorphology
    {
        public Closing()
        {
            kernel = new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            isDilation = true;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            isDilation = false;
            for (int i = 0; i < currImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                for (int j = 0; j < currImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
                }
            }
            return resultImage;
        }
    }
}
