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
            MakeBestPermutation(checkpoints, bestOrder.ToArray(), bestOrder);
            return bestOrder;
        }

        private static void MakeBestPermutation(Point[] checkpoints,
            int[] randomPermutation, int[] bestPermutation, int position = 0)
        {
            if(position == randomPermutation.Length)
            {
                var randomPermutationDistance =
                    GetPathLength(checkpoints, randomPermutation);
                var bestPermutationDistance =
                    GetPathLength(checkpoints, bestPermutation);
                if (randomPermutationDistance < bestPermutationDistance)
                    bestPermutation = randomPermutation.ToArray();
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
                        MakeBestPermutation(checkpoints, randomPermutation,
                            bestPermutation, position + 1);
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

        public static void MakePermutations(int[] permutation, int position, List<int[]> result)
        {
            if (position == permutation.Length)
            {
                result.Add(permutation.ToArray());
                return;
            }
            else
            {
                for (int i = 0; i < permutation.Length; i++)
                {
                    var index = Array.IndexOf(permutation, i, 0, position);
                    if (index == -1)
                    {
                        permutation[position] = i;
                        MakePermutations(permutation, position + 1, result);
                    }
                }
            }
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