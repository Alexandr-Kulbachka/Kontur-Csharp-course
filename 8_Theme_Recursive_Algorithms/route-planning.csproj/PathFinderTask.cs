using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrder = new List<int>();
            for (int i = 0; i < checkpoints.Length; i++)
                bestOrder.Add(i);
            return MakeTrivialPermutation(checkpoints, 1, new List<int>() { 0 }, bestOrder).ToArray();
        }

        public static bool IsThisWayBetter(Point[] checkpoints, List<int> bestOrder, List<int> permutation)
        {
            if (permutation.Count < 2) return true;
            var len1 = PointExtensions.GetPathLength(checkpoints, permutation.ToArray());
            var len2 = PointExtensions.GetPathLength(checkpoints, bestOrder.ToArray());
            return len1 < len2;
        }

        private static List<int> MakeTrivialPermutation(Point[] checkpoints, int position,
                                                        List<int> permutation, List<int> bestOrder)
        {
            if (permutation.Count == checkpoints.Length && IsThisWayBetter(checkpoints, bestOrder, permutation))
                bestOrder = new List<int>(permutation);
            else
                if (IsThisWayBetter(checkpoints, bestOrder, permutation))
            {
                permutation.Add(new int());
                for (int i = 1; i < checkpoints.Length; i++)
                    if (!permutation.Contains(i))
                    {
                        permutation[position] = i;
                        bestOrder = MakeTrivialPermutation(checkpoints, position + 1,
                                    permutation.ToList(), bestOrder.ToList());
                    }
            }
            return bestOrder;
        }
    }
}