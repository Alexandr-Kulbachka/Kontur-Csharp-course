using System.Linq;
using System;
using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double CalculatingTresholdValue(int theCounterOfThreshold, List<double> allPixels, int size)
        {
            if (theCounterOfThreshold == 0) return allPixels[size - 1] + 1;
            if (theCounterOfThreshold == size) return 0.0;
            var index = size - theCounterOfThreshold;
            return size % 2 == 0 ? (allPixels[index - 1] + allPixels[index]) / 2.0 : allPixels[index];
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var high = original.GetLength(1);
            var width = original.GetLength(0);
            var size = high * width;
            var allPixels = original.Cast<double>().ToList();
            allPixels.Sort();
            var theCounterOfThreshold = (int)(size * whitePixelsFraction);
            var thresholdValue = CalculatingTresholdValue(theCounterOfThreshold, allPixels, size);
            var result = new double[width, high];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < high; j++)
                    result[i, j] = original[i, j] >= thresholdValue ? 1.0 : 0.0;
            return result;
        }
    }
}