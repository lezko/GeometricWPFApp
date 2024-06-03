using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.library
{
    public class Polygon : GeometricFigure
    {
        public List<(double X, double Y)> Vertices { get; set; }

        public Polygon(double x, double y, List<(double X, double Y)> vertices) : base(x, y)
        {
            Vertices = vertices;
        }

        public override (double X1, double Y1, double X2, double Y2) GetBoundingRectangle()
        {
            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;

            foreach (var vertex in Vertices)
            {
                if (vertex.X < minX) minX = vertex.X;
                if (vertex.Y < minY) minY = vertex.Y;
                if (vertex.X > maxX) maxX = vertex.X;
                if (vertex.Y > maxY) maxY = vertex.Y;
            }

            return (minX, minY, maxX, maxY);
        }

        public override double GetArea()
        {
            double area = 0;
            int j = Vertices.Count - 1;

            for (int i = 0; i < Vertices.Count; i++)
            {
                area += (Vertices[j].X + Vertices[i].X) * (Vertices[j].Y - Vertices[i].Y);
                j = i;
            }

            return Math.Abs(area / 2);
        }
    }
}
