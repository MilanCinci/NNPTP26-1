using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber complexNumber)
        {           
            return new ComplexNumber()
            {
                RealPart = RealPart * complexNumber.RealPart - ImaginaryPart * complexNumber.ImaginaryPart,
                ImaginaryPart = RealPart * complexNumber.ImaginaryPart + ImaginaryPart * complexNumber.RealPart
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + complexNumber.RealPart,
                ImaginaryPart = ImaginaryPart + complexNumber.ImaginaryPart
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - complexNumber.RealPart,
                ImaginaryPart = ImaginaryPart - complexNumber.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber complexNumber)
        {
            var numerator = Multiply(new ComplexNumber() { RealPart = complexNumber.RealPart, ImaginaryPart = -complexNumber.ImaginaryPart });
            var denominator = complexNumber.RealPart * complexNumber.RealPart + complexNumber.ImaginaryPart * complexNumber.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = numerator.ImaginaryPart / denominator
            };
        }
    }
}
