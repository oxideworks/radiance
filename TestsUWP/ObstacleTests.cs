using System;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using Radiance.GameObjects;
using Radiance.GameObjects.Exceptions;
using Radiance.Primitives;

namespace TestsUWP
{
    [TestFixture]
    public class ObstacleTest
    {
        [Test]
        public void TestMethod1()
        {
            List<Vector> nodes = new List<Vector>();
            nodes.Add(new Vector(3));
            nodes.Add(new Vector(4));
            Assert.Throws(typeof(InvalidNumberOfNodesException), () =>
            {
                new Obstacle(new Polymer(nodes));
            });
        }
    }
}
