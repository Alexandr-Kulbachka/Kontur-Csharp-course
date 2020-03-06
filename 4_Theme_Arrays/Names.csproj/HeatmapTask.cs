using System;

namespace Names
{
    internal static class HeatmapTask
    {
        const int XSize = 30;
        const int YSize = 12;
        public static double[,] CalculateBirthsCounts(NameData[] names)
        {
            var birthsCounts = new double[XSize, YSize];
            foreach (var currentName in names)
                if (currentName.BirthDate.Day != 1)
                    birthsCounts[currentName.BirthDate.Day - 2, currentName.BirthDate.Month - 1]++;
            return birthsCounts;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var xLabels = new string[XSize];
            for (var y = 0; y < xLabels.Length; y++)
                xLabels[y] = (y + 2).ToString();
            var yLabels = new string[YSize];
            for (var y = 0; y < yLabels.Length; y++)
                yLabels[y] = (y + 1).ToString();
            return new HeatmapData(
                "Пример карты интенсивностей",
                CalculateBirthsCounts(names),
                xLabels,
                yLabels);
        }
    }
}
