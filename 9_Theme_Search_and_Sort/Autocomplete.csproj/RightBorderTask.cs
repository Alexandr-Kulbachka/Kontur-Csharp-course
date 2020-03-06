using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> items, string prefix, int left, int right)
        {
            while (right - left > 1)
            {
                var middle = left + (right - left) / 2;
                var lessOrEqual =
                    string.Compare(items[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0
                    || items[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
                if (lessOrEqual)
                    left = middle;
                else
                    right = middle;
            }

            return right;
        }
    }
}