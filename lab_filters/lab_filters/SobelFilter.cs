using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_filters
{
    class SobelFilter : MatrixFilter
    {
        public SobelFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i += 2)
            {
                for (int j = 0; j < sizeY; j += 2)
                    kernel[i, j] = i - 1;
                kernel[i, 1] = 2 * (i - 1);
            }
            for (int j = 0; j < sizeY; j++)
                kernel[1, j] = 0;
        }
    }
}
