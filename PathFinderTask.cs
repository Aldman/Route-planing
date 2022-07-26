using System;
using System.Collections.Generic;
using System.Drawing;


namespace RoutePlanning
{
	public static class PathFinderTask
	{
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            if (checkpoints.Length > 1)
            {
                var indexUsed = new List<int>() { 0 } ;
                for (int i = 0; i < checkpoints.Length; i++)
                {
                    AddIndexUsed(checkpoints,
                        indexUsed[indexUsed.Count - 1], indexUsed);
                }
                return indexUsed.ToArray();
            }
            else
            {
                return MakeTrivialPermutation(checkpoints.Length);
            }
		}

        public static void AddIndexUsed
            (Point[] checkpoints, int startIndex, List<int> indexUsed,
            double minDistance = double.MaxValue, int nextIndex = 0,
            int minIndex = 0)
        {
            if (nextIndex == checkpoints.Length)
            {
                indexUsed.Add(minIndex);
                return;
            }

            if (!indexUsed.Contains(nextIndex))
            {
                var randomDistance = PointExtensions
                    .DistanceTo(checkpoints[startIndex], checkpoints[minIndex]);
                if (randomDistance < minDistance)
                {
                    minIndex = nextIndex;
                    minDistance = randomDistance;
                }
            }
            AddIndexUsed(checkpoints, startIndex, indexUsed,
                minDistance, nextIndex + 1, minIndex);
        }

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}
	}
}