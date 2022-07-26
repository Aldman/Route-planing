using System;
using System.Drawing;
using System.Collections.Generic;
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
                    new Point(0, 0),
                    new Point(1, 1),
                    new Point(2, 2),
                };
                int startIndex = 0;
                var indexUsed = new List<int>() { 0 };
                var expectedResult = new List<int>()
                { 0, 1};
                yield return new TestCaseData(checkpoints, startIndex,
                    indexUsed, expectedResult)
                    .SetName("Simple test to near point");

                indexUsed = new List<int>() { 0, 1 };
                expectedResult = new List<int>()
                { 0, 1, 2};
                yield return new TestCaseData(checkpoints, startIndex,
                    indexUsed, expectedResult)
                    .SetName("Test with indexUsed 1");

                indexUsed = new List<int>() { 0, 2 };
                expectedResult = new List<int>()
                { 0, 2, 1};
                yield return new TestCaseData(checkpoints, startIndex,
                    indexUsed, expectedResult);
            }
        }

        [TestCaseSource("PathFinderCaseData")]
        public void AddIndexUsedTest
            (Point[] checkpoints, int startIndex, 
            List<int> indexUsed, List<int> expectedResult)
        {
            PathFinderTask.AddIndexUsed(checkpoints, startIndex, indexUsed);
            Assert.AreEqual(expectedResult, indexUsed);
        }
    }
}
