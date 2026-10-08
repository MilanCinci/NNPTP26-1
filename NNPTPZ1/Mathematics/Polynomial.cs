using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial derivedPolynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivedPolynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivedPolynomial;
        }

        public ComplexNumber Evaluate(double value)
        {
            return Evaluate(new ComplexNumber
            {
                RealPart = value,
                ImaginaryPart = 0
            });
        }

        public ComplexNumber Evaluate(ComplexNumber complexNumber)
        {
            ComplexNumber result  = ComplexNumber.Zero;
            for (int power = 0; power < Coefficients.Count; power++)
            {
                ComplexNumber coefficient = Coefficients[power];
                ComplexNumber currentX = complexNumber;

                if (power > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        currentX = currentX.Multiply(complexNumber);

                    coefficient = coefficient.Multiply(currentX);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    result += " + ";
            }

            return result;
        }
    }
}
