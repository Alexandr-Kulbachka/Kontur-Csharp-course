using System.Collections.Generic;
using System.Linq;
using System;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double NewValueAfterFilter(List<double> neighboringValues)
        {
            var size = neighboringValues.Count;
            return size % 2 == 0 ? (neighboringValues[size / 2] +
                                  neighboringValues[(size / 2) - 1]) / 2.0 :
            neighboringValues[size / 2];
        }

        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var afterFilter = new double[width, height];
            var neighboringValues = new List<double>();
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    var startW = (i - 1) >= 0 ? i - 1 : i;
                    var finishW = (i + 1) < width ? i + 1 : i;
                    var startH = (j - 1) >= 0 ? j - 1 : j;
                    var finishH = (j + 1) < height ? j + 1 : j;
                    for (var a = startW; a <= finishW; a++)
                        for (var b = startH; b <= finishH; b++)
                            neighboringValues.Add(original[a, b]);
                    neighboringValues.Sort();
                    afterFilter[i, j] = NewValueAfterFilter(neighboringValues);
                    neighboringValues.Clear();
                }
            return afterFilter;
        }
    }
}