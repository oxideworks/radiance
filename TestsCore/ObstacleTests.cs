using NUnit.Framework;
using RadianceStandard.GameObjects;
using RadianceStandard.GameObjects.Exceptions;
using RadianceStandard.Primitives;
using System;
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
        public bool NodeInsideOrOutsideFirstQuarterRightTriangle(float vecX, float vecY)
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
        public bool NodeInsideOrOutsideThirdQuarterRightTriangle(float vecX, float vecY)
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
        public bool NodeInsideOrOutsideParallelToXAxisIsoscelesTriangle(float vecX, float vecY)
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
        public bool NodeInsideOrOutsideOctagon(float vecX, float vecY)
        {
            return _obstacleOctagon.Contains(new Vector(vecX, vecY));
        }
        

        #endregion


        [TestCase]
        public void OnObstacleSpawnLackOfNodesCase()
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

        //[TestCase]
        //public bool ObstacleIntersectsObstacle(Obstacle a, Obstacle b)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
