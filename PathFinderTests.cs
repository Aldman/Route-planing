using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RoutePlanning
{
    [TestFixture]
    class PathFinderTests
    {
        private static IEnumerable<TestCaseData> PathFinderCaseData
        {
            get
            {
                var checkpoints = new Point[]
                {
                    new Point(1, 0),
                    new Point(1, 1),
                    new Point(0, 0),
                };
                int[] bestPermutation = PathFinderTask
                    .MakeTrivialPermutation(checkpoints.Length);
                var bestDistance = double.MaxValue;
                double expectedResult = 0;
                yield return new TestCaseData(checkpoints, bestPermutation.ToArray(),
                    bestPermutation, bestDistance, expectedResult, 0);

                //indexUsed = new List<int>() { 0, 1 };
                //expectedResult = new List<int>()
                //{ 0, 1, 2};
                //yield return new TestCaseData(checkpoints, startIndex,
                //    indexUsed, expectedResult)
                //    .SetName("Test with indexUsed 1");

                //indexUsed = new List<int>() { 0, 2 };
                //expectedResult = new List<int>()
                //{ 0, 2, 1};
                //yield return new TestCaseData(checkpoints, startIndex,
                //    indexUsed, expectedResult);
            }
        }

        [TestCaseSource("PathFinderCaseData")]
        public void MakeBestPermutationTest(Point[] checkpoints,
            int[] randomPermutation, int[] bestPermutation,
            double bestDistance, double expectedResult, int position = 0)
        {
            PathFinderTask
                .MakeBestPermutation(checkpoints, randomPermutation,
                ref bestPermutation, ref bestDistance);
            Assert.AreEqual(expectedResult, bestDistance);
        }
    }
}
