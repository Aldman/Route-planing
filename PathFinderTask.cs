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
            var bestOrder = MakeTrivialPermutation(checkpoints.Length);
            var bestDistance = double.MaxValue;
            MakeBestPermutation(checkpoints, bestOrder.ToArray(),
                ref bestOrder, ref bestDistance);
            return bestOrder;
        }

        public static void MakeBestPermutation(Point[] checkpoints,
            int[] randomPermutation, ref int[] bestPermutation,
            ref double bestDistance, int position = 0)
        {
            if(position == randomPermutation.Length)
            {
                var randomPermutationDistance =
                    GetPathLength(checkpoints, randomPermutation);
                var bestPermutationDistance =
                    GetPathLength(checkpoints, bestPermutation);
                if (randomPermutationDistance < bestPermutationDistance)
                {
                    bestDistance = randomPermutationDistance;
                    bestPermutation = randomPermutation.ToArray();
                }
                return;
            }
            else
            {
                for (int i = 0; i < randomPermutation.Length; i++)
                {
                    var index = Array.IndexOf(randomPermutation, i, 0, position);
                    if (index == -1)
                    {
                        randomPermutation[position] = i;
                        if (position > 0)
                        {
                            var currentState = new int[position + 1];
                            Array.Copy(randomPermutation, currentState, position + 1);
                            var currentDistance = GetPathLength(checkpoints, currentState);
                            if (currentDistance > bestDistance)
                                return;
                        }
                        MakeBestPermutation(checkpoints, randomPermutation,
                            ref bestPermutation, ref bestDistance, position + 1);
                    }
                }
            }
        }

        private static double GetPathLength(Point[] checkpoints, int[] permutation)
        {
            double totalDistance = 0;
            if (permutation.Length > 1)
            {
                for (int i = 1; i < permutation.Length; i++)
                {
                    var pointA = checkpoints[permutation[i - 1]];
                    var pointB = checkpoints[permutation[i]];
                    totalDistance += PointExtensions
                        .DistanceTo(pointA, pointB);
                }
            }
            return totalDistance;
        }

        public static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}
	}
}