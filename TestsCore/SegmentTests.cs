using NUnit.Framework;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace TestsCore
{
    [TestFixture]
    public class SegmentTests
    {
        #region Crossing Point
        private static IEnumerable<(Segment, Segment, Vector)> CrossingPointSource
        {
            get
            {
                var segment1 = new Segment(new Vector(0, 2), new Vector(0, -2));
                var segment2 = new Segment(new Vector(-1, 0), new Vector(3, 0));
                var segment3 = new Segment(new Vector(3, 0), new Vector(-1, 0));
                var segment4 = new Segment(new Vector(-1, 1), new Vector(3, 1));
                var crossingPoint = new Vector(0);

                yield return (segment1, segment2, crossingPoint);
                yield return (segment1, segment3, crossingPoint);
                yield return (segment2, segment4, null);
            }
        }

        [TestCaseSource(nameof(CrossingPointSource))]
        public void TestSegmentCrossing((Segment, Segment, Vector) bundle)
        {
            var (s1, s2, expected) = bundle;
            if (expected != null)
            {
                Assert.IsTrue(s1.TryFindCrossingPoint(s2, out Vector received));
                Assert.IsTrue(expected == received);
                Assert.IsTrue(s2.TryFindCrossingPoint(s1, out Vector received2));
                Assert.IsTrue(expected == received2);
            }
            else
            {
                Assert.IsFalse(s1.TryFindCrossingPoint(s2, out Vector received));
                Assert.IsNull(received);
                Assert.IsFalse(s2.TryFindCrossingPoint(s1, out Vector received2));
                Assert.IsNull(received2);
            }
        }
        #endregion
    }
}
