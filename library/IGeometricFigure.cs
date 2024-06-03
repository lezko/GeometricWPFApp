using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.library
{
    public interface IGeometricFigure
    {
        (double X1, double Y1, double X2, double Y2) GetBoundingRectangle();
        double GetArea();
    }
}
