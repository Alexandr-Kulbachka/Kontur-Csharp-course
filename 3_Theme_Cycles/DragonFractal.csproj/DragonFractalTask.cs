using System;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
		static double fortyFiveDegreesCos = Math.Cos(Math.PI / 4);
		static double fortyFiveDegreesSin = Math.Sin(Math.PI / 4);
		static double oneHundredAndThirtyFiveDegreesCos = Math.Cos(3 * Math.PI / 4);
		static double oneHundredAndThirtyFiveDegreesSin = Math.Sin(3 * Math.PI / 4);
        public static void GenerateNewCoordinate(double x, double y, ref double nextX, 
													   ref double nextY, int random)
        {
            switch (random)
            {
                case 0:
                    nextX = (x * fortyFiveDegreesCos - y * fortyFiveDegreesSin) / Math.Sqrt(2);
                    nextY = (x * fortyFiveDegreesSin + y * fortyFiveDegreesCos) / Math.Sqrt(2);
                    break;
                case 1:
                    nextX = (x * oneHundredAndThirtyFiveDegreesCos - 
							 y * oneHundredAndThirtyFiveDegreesSin) / Math.Sqrt(2) + 1;
                    nextY = (x * oneHundredAndThirtyFiveDegreesSin + 
							 y * oneHundredAndThirtyFiveDegreesCos) / Math.Sqrt(2);
                    break;
            }
        }
		
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var x = 1.0;
            var y = 0.0;
            var nextX = 0.0;
            var nextY = 0.0;
            Random random = new Random(seed);
            for (int i = 0; i <= iterationsCount; i++)
            {
                pixels.SetPixel(x, y);
                GenerateNewCoordinate(x, y, ref nextX, ref nextY, random.Next(2));
                x = nextX;
                y = nextY;
            }           
        }
    }
}