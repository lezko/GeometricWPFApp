using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.library
{
    public class Point : GeometricFigure
    {
        public Point(double x, double y) : base(x, y) { }

        public override (double X1, double Y1, double X2, double Y2) GetBoundingRectangle()
        {
            return (X, Y, X, Y);
        }

        public override double GetArea()
        {
            return 0;
        }
    }
}
