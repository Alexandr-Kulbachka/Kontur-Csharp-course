using System;
namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] Transpose(double[,] sx)
        {
            var size = sx.GetLength(0);
            var sy = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sy[j, i] = sx[i, j];
                }
            }
            return sy;
        }

        public static double CalculateTheGradient(double[,] g, double[,] sx, double[,] sy, int x, int y,
        int sizeOfGradientMatrix, int sizeOfPointNeighborhood)
        {
            double gx = 0;
            double gy = 0;
            for (int i = 0; i < sizeOfGradientMatrix; i++)
                for (int j = 0; j < sizeOfGradientMatrix; j++)
                {
                    gx += sx[i, j] * g[(x - sizeOfPointNeighborhood + i), (y - sizeOfPointNeighborhood + j)];
                    gy += sy[i, j] * g[(x - sizeOfPointNeighborhood + i), (y - sizeOfPointNeighborhood + j)];
                }
            return Math.Sqrt(gx * gx + gy * gy);
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sizeOfGradientMatrix = sx.GetLength(0);
            var sizeOfPointNeighborhood = (sizeOfGradientMatrix - 1) / 2;
            var sy = Transpose(sx);
            for (int x = sizeOfPointNeighborhood; x < width - sizeOfPointNeighborhood; x++)
                for (int y = sizeOfPointNeighborhood; y < height - sizeOfPointNeighborhood; y++)
                    result[x, y] = CalculateTheGradient(g, sx, sy, x, y, sizeOfGradientMatrix, sizeOfPointNeighborhood);
            return result;
        }
    }
}

