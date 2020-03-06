using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        struct Vector
        {
            public double X;
            public double Y;
        }
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            Vector vectorBC = new Vector { X = cX - bX, Y = cY - bY };
            Vector vectorBA = new Vector { X = aX - bX, Y = aY - bY };
            var firstScalarProductOfVectors = vectorBC.X * vectorBA.X + vectorBC.Y * vectorBA.Y;

            Vector vectorAC = new Vector { X = cX - aX, Y = cY - aY };
            Vector vectorAB = new Vector { X = bX - aX, Y = bY - aY };
            var secondScalarProductOfVectors = vectorAC.X * vectorAB.X + vectorAC.Y * vectorAB.Y;

            var sideAC = Math.Sqrt((cX - aX) * (cX - aX) + (cY - aY) * (cY - aY));
            var sideBC = Math.Sqrt((cX - bX) * (cX - bX) + (cY - bY) * (cY - bY));
            if (firstScalarProductOfVectors > 0 && secondScalarProductOfVectors > 0)
            {
                var sideAB = Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY));
                var halfOfPerimetr = (sideAB + sideBC + sideAC) / 2;
                var square = Math.Sqrt(halfOfPerimetr *
                                     (halfOfPerimetr - sideAB) *
                                     (halfOfPerimetr - sideAC) *
                                     (halfOfPerimetr - sideBC));
                return (2 * square) / sideAB;
            }
            else return Math.Min(sideAC, sideBC);
        }
    }
}