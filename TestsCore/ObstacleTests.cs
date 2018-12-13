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
        private static List<Vector> _nodes = new List<Vector>
        {
            new Vector(0, 0),
            new Vector(0, 2),
            new Vector(2, 0)
        };

        private IObstacle _obstacle = new Obstacle(new Polymer(_nodes));

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

        [TestCase(1.5f, 0.5f, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(2, 0, ExpectedResult = false)]
        [TestCase(2, 2, ExpectedResult = false)]
        [TestCase(0, 1, ExpectedResult = false)]
        [TestCase(200, 200, ExpectedResult = false)]
        [TestCase(-200, -200, ExpectedResult = false)]
        [TestCase(1f, 1f, ExpectedResult = false)]
        [TestCase(0, 200, ExpectedResult = false)]
        [TestCase(2, -200, ExpectedResult = false)]
        [TestCase(.5f, .5f, ExpectedResult = true)]
        public bool NodeInsideOrOutside(float vecX, float vecY)
        {
            return _obstacle.Contains(new Vector(vecX, vecY));
        }

        [TestCase]
        public bool ObstacleIntersectsObstacle(Obstacle a, Obstacle b)
        {
            throw new NotImplementedException();
        }
    }
}
