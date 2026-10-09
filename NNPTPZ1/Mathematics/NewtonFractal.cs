using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class NewtonFractal
    {
        private const int MaxIterationsCount = 30;
        private const double PolynomialRootsDistanceThreshold = 0.01;
        private const double NewtonIterationsThreshold = 0.5;
        private const double ZeroCoordinateReplacement = 0.0001;

        private Color[] colors;
        private Bitmap bitmapImage;
        private double xMin;
        private double yMin;
        private double xMax;
        private double yMax;
        private double xStep;
        private double yStep;
        private int imageWidth;
        private int imageHeight;
        private List<ComplexNumber> polynomialRoots;
        private Polynomial polynomial;
        private Polynomial derivedPolynomial;
        private string output;

        public void GenerateNewtonFractal(int[] imageDimensions, double[] imageSizeBoundaries, string output)
        {
            InitializeCalculation(imageDimensions, imageSizeBoundaries, output);

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            ColorizePixels();

            bitmapImage.Save(this.output ?? "../../../out.png");
        }

        private void InitializeCalculation(int[] imageDimensions, double[] imageSizeBoundaries, string output)
        {
            imageWidth = imageDimensions[0];
            imageHeight = imageDimensions[1];

            xMin = imageSizeBoundaries[0];
            xMax = imageSizeBoundaries[1];
            yMin = imageSizeBoundaries[2];
            yMax = imageSizeBoundaries[3];

            this.output = output;

            xStep = Math.Abs(xMax - xMin) / imageWidth;
            yStep = Math.Abs(yMax - yMin) / imageHeight;
           
            bitmapImage = new Bitmap(imageWidth, imageHeight);
            polynomialRoots = new List<ComplexNumber>();

            colors = InitializeColors();
            polynomial = InitializePolynomial();
            derivedPolynomial = polynomial.Derive();
        }

        private void ColorizePixels()
        {
            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber currentPoint = InitializeComplexPoint(x, y);

                    int iterationCount = 0;

                    currentPoint = CalculateNewtonIterations(polynomial, derivedPolynomial, currentPoint, out iterationCount);
                    int rootIndex = FindRootIndex(currentPoint, polynomialRoots);
                    Color pixelColor = GetPixelColor(colors, rootIndex, iterationCount);
                    bitmapImage.SetPixel(j, i, pixelColor);
                }
            }
        }

        private static Color[] InitializeColors()
        {
            return new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
                Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }

        private static Polynomial InitializePolynomial()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });

            return polynomial;
        }

        private static ComplexNumber InitializeComplexPoint(double x, double y)
        {
            ComplexNumber point = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (point.RealPart == 0)
                point.RealPart = ZeroCoordinateReplacement;

            if (point.ImaginaryPart == 0)
                point.ImaginaryPart = ZeroCoordinateReplacement;

            return point;
        }

        private static ComplexNumber CalculateNewtonIterations(Polynomial polynomial, Polynomial derivedPolynomial, ComplexNumber currentPoint, out int iterationsCount)
        {
            iterationsCount = 0;
            for (int i = 0; i < MaxIterationsCount; i++)
            {
                var currentPointDifference = polynomial.Evaluate(currentPoint).Divide(derivedPolynomial.Evaluate(currentPoint));
                currentPoint = currentPoint.Subtract(currentPointDifference);

                if (Math.Pow(currentPointDifference.RealPart, 2) + Math.Pow(currentPointDifference.ImaginaryPart, 2) >= NewtonIterationsThreshold)
                {
                    i--;
                }
                iterationsCount++;
            }

            return currentPoint;
        }

        private static int FindRootIndex(ComplexNumber currentPoint, List<ComplexNumber> polynomialRoots)
        {
            bool isRootIndexFound = false;
            int rootIndex = 0;
            for (int i = 0; i < polynomialRoots.Count; i++)
            {
                if (Math.Pow(currentPoint.RealPart - polynomialRoots[i].RealPart, 2) + 
                    Math.Pow(currentPoint.ImaginaryPart - polynomialRoots[i].ImaginaryPart, 2) <= PolynomialRootsDistanceThreshold)
                {
                    isRootIndexFound = true;
                    rootIndex = i;
                }
            }
            if (!isRootIndexFound)
            {
                polynomialRoots.Add(currentPoint);
                rootIndex = polynomialRoots.Count;    
            }

            return rootIndex;
        }

        private static Color GetPixelColor(Color[] colors, int rootIndex, int iterationCount)
        {
            Color pixelColor = colors[rootIndex % colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - iterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterationCount * 2), 255));

            return pixelColor;
        }
    }
}
