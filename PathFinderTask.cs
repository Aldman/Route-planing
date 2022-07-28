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
            ref double bestDistance, int position = 1)
        {
            if(position == randomPermutation.Length)
            {
                var randomPermutationDistance =
                    PointExtensions.GetPathLength(checkpoints, randomPermutation);
                var bestPermutationDistance =
                    PointExtensions.GetPathLength(checkpoints, bestPermutation);
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
                            var currentDistance = PointExtensions.GetPathLength(checkpoints, currentState);
                            if (currentDistance > bestDistance)
                                return;
                        }
                        MakeBestPermutation(checkpoints, randomPermutation,
                            ref bestPermutation, ref bestDistance, position + 1);
                    }
                }
            }
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