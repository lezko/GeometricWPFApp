using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.library
{
    public abstract class GeometricFigure : IGeometricFigure
    {
        public double X { get; set; }
        public double Y { get; set; }

        public GeometricFigure(double x, double y)
        {
            X = x;
            Y = y;
        }

        public abstract (double X1, double Y1, double X2, double Y2) GetBoundingRectangle();
        public abstract double GetArea();
    }
}
