using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ImageDimensionCount = 2;
            const int ImageSizeBoundariesCount = 4;
            const int OutputArgumentIndex = ImageDimensionCount + ImageSizeBoundariesCount;

            int[] imageDimensions = new int[ImageDimensionCount];           
            for (int i = 0; i < imageDimensions.Length; i++)
            {
                imageDimensions[i] = int.Parse(args[i]);
            }

            double[] imageSizeBoundaries = new double[ImageSizeBoundariesCount];
            for (int i = 0; i < imageSizeBoundaries.Length; i++)
            {
                imageSizeBoundaries[i] = double.Parse(args[i + ImageDimensionCount]);
            }

            string output = args[OutputArgumentIndex];

            NewtonFractal newtonFractal = new NewtonFractal();
            newtonFractal.GenerateNewtonFractal(imageDimensions, imageSizeBoundaries, output);
        }
    }
}
