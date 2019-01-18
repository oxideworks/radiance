using NUnit.Framework;
using RadianceStandard.Primitives;
using System;

namespace TestsCore.PrimitivesTests
{
    [TestFixture]
    public class TriangleTests
    {
        private static readonly Triangle testTriangle1 =
            new Triangle(
                new Vector(1, 1),
                new Vector(9, 1),
                new Vector(9, 5)
            );

        [TestCase]
        public void CenterTest1()
        {
            Assert.AreEqual(new Vector(5, 3), testTriangle1.CircleCenter);
        }

        [TestCase]
        public void RadiusTest1()
        {
            var myRadius = (testTriangle1.CircleCenter - testTriangle1.Polymer[0]).Length;
            var expected = (float)Math.Sqrt(20);
            Assert.AreEqual(expected, myRadius);
            Assert.AreEqual(expected, testTriangle1.CircleRadius);
        }

        [TestCase]
        public void RadiusSqTest1()
        {
            var myRadiusSq = (testTriangle1.CircleCenter - testTriangle1.Polymer[0]).LengthSquared;
            Assert.AreEqual(20, myRadiusSq);
            Assert.AreEqual(20, testTriangle1.CircleRadiusSquared);
        }
    }
}
