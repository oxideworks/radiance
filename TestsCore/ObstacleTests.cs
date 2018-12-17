using NUnit.Framework;
using RadianceStandard.Exceptions;
using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace TestsCore
{
    [TestFixture]
    public class ObstacleTest
    {
        #region FirstQuarterRightTriangleTests
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
        public bool TestIfNodeIsInsideOrOutsideFirstQuarterRightTriangle(float vecX, float vecY)
        {
            return _obstacleFirstQuarterRightTriangle.Contains(new Vector(vecX, vecY));
        }

        #endregion

        #region ThirdQuarterRightTriangleTests
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

        #region IsoscelesParallelToXAxisTriangleTests

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

        #region OctagonTests

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
    }
}
