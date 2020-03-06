using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var minDay = 1;
            var maxDay = 31;

            var days = new string[maxDay];
            for (var y = 0; y < days.Length; y++)
                days[y] = (y + minDay).ToString();
            var birthsCounts = new double[maxDay];
            foreach (var currentName in names)
                if (currentName.Name == name && currentName.BirthDate.Day != 1)
                    birthsCounts[currentName.BirthDate.Day - 1]++;

            return new HistogramData(
                $"Рождаемость людей с именем {name}",
                days,
                birthsCounts);
        }
    }
}