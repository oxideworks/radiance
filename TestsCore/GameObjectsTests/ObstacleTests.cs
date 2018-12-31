using NUnit.Framework;
using RadianceStandard.Exceptions;
using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace TestsCore.GameObjectsTests
{
    [TestFixture]
    public class ObstacleTest
    {
        #region Contains Point: FirstQuarterRightTriangleTests
        private static readonly List<Vector> _firstQuarterRightTriangleNodes = new List<Vector>
        {
            new Vector(0, 0),
            new Vector(0, 2),
            new Vector(2, 0)
        };

        private static readonly IObstacle _obstacleFirstQuarterRightTriangle
            = new Obstacle(new Polymer(_firstQuarterRightTriangleNodes));

        [TestCase(1.5f, 0.5f, ExpectedResult = true)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(2, 0, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = false)]
        [TestCase(200, 200, ExpectedResult = false)]
        [TestCase(-200, -200, ExpectedResult = false)]
        [TestCase(1f, 1f, ExpectedResult = true)]
        [TestCase(0, 200, ExpectedResult = false)]
        [TestCase(2, -200, ExpectedResult = false)]
        [TestCase(.5f, .5f, ExpectedResult = true)]
        [TestCase(3, 0, ExpectedResult = false)]
        [TestCase(3, 1, ExpectedResult = false)]
        [TestCase(-3, 0, ExpectedResult = false)]
        public bool TestIfNodeIsInsideOrOutsideFirstQuarterRightTriangle(float vecX, float vecY)
        {
            return _obstacleFirstQuarterRightTriangle.Contains(new Vector(vecX, vecY));
        }

        #endregion

        #region Contains Point: ThirdQuarterRightTriangleTests
        private static readonly List<Vector> _thirdQuarterRightTriangleNodes = new List<Vector>
        {
            new Vector(0, 0),
            new Vector(0, -2),
            new Vector(-2, 0)
        };

        private static readonly IObstacle _obstacleThirdQuarterRightTriangle
            = new Obstacle(new Polymer(_thirdQuarterRightTriangleNodes));

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(-1, 0, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-.5f, -.5f, ExpectedResult = true)]
        [TestCase(0, -1, ExpectedResult = true)]
        [TestCase(0, -2, ExpectedResult = true)]
        [TestCase(-2, -2, ExpectedResult = false)]
        [TestCase(-20, -20, ExpectedResult = false)]
        [TestCase(2, -1, ExpectedResult = false)]
        public bool TestIfNodeIsInsideOrOutsideThirdQuarterRightTriangle(float vecX, float vecY)
        {
            return _obstacleThirdQuarterRightTriangle.Contains(new Vector(vecX, vecY));
        }

        #endregion

        #region Contains Point: IsoscelesParallelToXAxisTriangleTests

        private static readonly List<Vector> _isoscelesParallelToXAxisTriangleNodes = new List<Vector>
        {
            new Vector(-2, 0),
            new Vector(2, 0),
            new Vector(0, -2)
        };

        private static readonly IObstacle _obstacleParallelToXAxisIsoscelesTriangle
            = new Obstacle(new Polymer(_isoscelesParallelToXAxisTriangleNodes));

        [TestCase(0f, 0f, ExpectedResult = true)]
        [TestCase(0f, 1f, ExpectedResult = false)]
        [TestCase(0f, -2f, ExpectedResult = true)]
        [TestCase(0f, -1f, ExpectedResult = true)]
        [TestCase(0f, -3f, ExpectedResult = false)]
        [TestCase(2f, 0f, ExpectedResult = true)]
        [TestCase(-2f, 0f, ExpectedResult = true)]
        [TestCase(0f, -2f, ExpectedResult = true)]
        [TestCase(1f, -1f, ExpectedResult = true)]
        [TestCase(-1f, -1f, ExpectedResult = true)]
        public bool TestIfNodeIsInsideOrOutsideParallelToXAxisIsoscelesTriangle(float vecX, float vecY)
        {
            return _obstacleParallelToXAxisIsoscelesTriangle.Contains(new Vector(vecX, vecY));
        }

        #endregion

        #region Contains Point: OctagonTests

        private static readonly List<Vector> _OctagonNodes = new List<Vector>
        {
            new Vector(-1, 0),
            new Vector(-2, 1),
            new Vector(0, 2),
            new Vector(2, 1),
            new Vector(1, 0),
            new Vector(2, -1),
            new Vector(0, -2),
            new Vector(-2, -1)
        };

        private static readonly IObstacle _obstacleOctagon
            = new Obstacle(new Polymer(_OctagonNodes));

        [TestCase(-1f, 1f, ExpectedResult = true)]
        [TestCase(1f, 1f, ExpectedResult = true)]
        [TestCase(0f, 0f, ExpectedResult = true)]
        [TestCase(-1f, -1f, ExpectedResult = true)]
        [TestCase(1f, -1f, ExpectedResult = true)]
        [TestCase(-1.5f, 0f, ExpectedResult = false)]
        [TestCase(1.5f, 0f, ExpectedResult = false)]
        [TestCase(-2f, 0f, ExpectedResult = false)]
        [TestCase(2f, 0f, ExpectedResult = false)]
        [TestCase(0f, -20f, ExpectedResult = false)]
        [TestCase(0f, 20f, ExpectedResult = false)]
        public bool TestIfNodeIsInsideOrOutsideOctagon(float vecX, float vecY)
        {
            return _obstacleOctagon.Contains(new Vector(vecX, vecY));
        }
        #endregion


        #region Spawn Test
        [TestCase]
        public void TestSpawningWithLackOfNodes()
        {
            List<Vector> nodes = new List<Vector>
            {
                new Vector(3),
                new Vector(4)
            };
            Assert.Throws(typeof(InvalidNumberOfNodesException), () =>
            {
                new Obstacle(new Polymer(nodes));
            });
        }
        #endregion

        #region Intersects Test
        private static IEnumerable<(Obstacle, Obstacle, bool)> ObstacleIntersectsSoruce
        {
            get
            {
                var poly1 = new Polymer(new[] { new Vector(0), new Vector(0, 1), new Vector(1, .5f) });
                var poly2 = new Polymer(new[] { new Vector(0, .5f), new Vector(1, 1), new Vector(1, 0) });
                var poly3 = new Polymer(new[] { new Vector(1), new Vector(2), new Vector(2, 0) });
                yield return (new Obstacle(poly1), new Obstacle(poly2), true);
                yield return (new Obstacle(poly1), new Obstacle(poly3), false);
            }
        }

        [TestCaseSource(nameof(ObstacleIntersectsSoruce))]
        public void TestObstacleIntersectsObstacle((Obstacle, Obstacle, bool) bundle)
        {
            var (a, b, expected) = bundle;
            if (expected)
            {
                Assert.IsTrue(a.Intersects(b));
                Assert.IsTrue(b.Intersects(a));
            }
            else
            {
                Assert.IsFalse(a.Intersects(b));
                Assert.IsFalse(b.Intersects(a));
            }
        }
        #endregion

        #region Partially contains obstacle test
        private static IEnumerable<(Obstacle, Obstacle, bool)> ObstaclePartiallyContainsObstacleSource
        {
            get
            {
                var outer1 = new Polymer(new[] { new Vector(0), new Vector(0, 4), new Vector(4), new Vector(4, 0) });
                var inner1 = new Polymer(new[] { new Vector(1), new Vector(1, 3), new Vector(3), new Vector(3, 1) });
                var inner2 = new Polymer(new[] { new Vector(4, 0), new Vector(4, 4), new Vector(8, 4), new Vector(8, 0) });
                var inner3 = new Polymer(new[] { new Vector(4.001f, 0), new Vector(4.001f, 4), new Vector(8, 4), new Vector(8, 0) });
                var inner4 = new Polymer(new[] { new Vector(3, 0), new Vector(3, 4), new Vector(8, 4), new Vector(8, 0) });

                yield return (new Obstacle(outer1), new Obstacle(inner1), true);
                yield return (new Obstacle(inner1), new Obstacle(outer1), false);
                yield return (new Obstacle(inner1), new Obstacle(inner1), true);
                yield return (new Obstacle(outer1), new Obstacle(inner2), true);
                yield return (new Obstacle(outer1), new Obstacle(inner3), false);
                yield return (new Obstacle(outer1), new Obstacle(inner4), true);
            }
        }

        [TestCaseSource(nameof(ObstaclePartiallyContainsObstacleSource))]
        public void TestObstaclePartiallyContainsObstacle((Obstacle, Obstacle, bool) bundle)
        {
            var (outer, inner, expected) = bundle;
            if (expected)
            {
                Assert.IsTrue(outer.PartiallyContains(inner));
            }
            else
            {
                Assert.IsFalse(outer.PartiallyContains(inner));
            }
        }
        #endregion

        #region Completely contains obstacle test
        private static IEnumerable<(Obstacle, Obstacle, bool)> ObstacleCompletelyContainsObstacleSource
        {
            get
            {
                var outer1 = new Polymer(new[] { new Vector(0), new Vector(0, 4), new Vector(4), new Vector(4, 0) });
                var inner1 = new Polymer(new[] { new Vector(1), new Vector(1, 3), new Vector(3), new Vector(3, 1) });
                var inner2 = new Polymer(new[] { new Vector(4, 0), new Vector(4, 4), new Vector(8, 4), new Vector(8, 0) });
                var inner3 = new Polymer(new[] { new Vector(4.001f, 0), new Vector(4.001f, 4), new Vector(8, 4), new Vector(8, 0) });
                var inner4 = new Polymer(new[] { new Vector(3, 0), new Vector(3, 4), new Vector(8, 4), new Vector(8, 0) });

                yield return (new Obstacle(outer1), new Obstacle(inner1), true);
                yield return (new Obstacle(inner1), new Obstacle(outer1), false);
                yield return (new Obstacle(inner1), new Obstacle(inner1), true);
                yield return (new Obstacle(outer1), new Obstacle(inner2), false);
                yield return (new Obstacle(outer1), new Obstacle(inner3), false);
                yield return (new Obstacle(outer1), new Obstacle(inner4), false);
            }
        }

        [TestCaseSource(nameof(ObstacleCompletelyContainsObstacleSource))]
        public void TestObstacleCompletelyContainsObstacle((Obstacle, Obstacle, bool) bundle)
        {
            var (outer, inner, expected) = bundle;
            if (expected)
            {
                Assert.IsTrue(outer.CompletelyContains(inner));
            }
            else
            {
                Assert.IsFalse(outer.CompletelyContains(inner));
            }
        }
        #endregion
    }
}
