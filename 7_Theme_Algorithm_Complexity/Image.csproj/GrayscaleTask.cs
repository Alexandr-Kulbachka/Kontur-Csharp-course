namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var grayscale = new double[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    grayscale[i, j] = (0.299 * original[i, j].R +
                                       0.587 * original[i, j].G +
                                       0.114 * original[i, j].B
                                      ) / 255;
                }
            return grayscale;
        }
    }
}