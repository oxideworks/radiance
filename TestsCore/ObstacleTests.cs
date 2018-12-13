﻿using NUnit.Framework;
using RadianceStandard.GameObjects;
using RadianceStandard.GameObjects.Exceptions;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace TestsCore
{
    [TestFixture]
    public class ObstacleTest
    {
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
    }
}