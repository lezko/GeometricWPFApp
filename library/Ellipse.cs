using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.library
{
    public class Ellipse : GeometricFigure
    {
        public double MajorAxis { get; set; }
        public double MinorAxis { get; set; }

        public Ellipse(double x, double y, double majorAxis, double minorAxis) : base(x, y)
        {
            MajorAxis = majorAxis;
            MinorAxis = minorAxis;
        }

        public override (double X1, double Y1, double X2, double Y2) GetBoundingRectangle()
        {
            return (X - MajorAxis, Y - MinorAxis, X + MajorAxis, Y + MinorAxis);
        }

        public override double GetArea()
        {
            return Math.PI * MajorAxis * MinorAxis;
        }
    }
}
