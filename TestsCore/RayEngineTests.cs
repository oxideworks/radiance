using NUnit.Framework;
using RadianceStandard.Primitives;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestsCore
{
    [TestFixture]
    public class RayEngineTests
    {
        static private IEnumerable<(Ray, Ray, Vector)> RayPairsSource
        {
            get
            {
                var r1 = new Ray(new Vector(2, 4), new Vector(11, 2));
                var r2 = new Ray(new Vector(10, 1), new Vector(12, 3));
                var r3 = new Ray(new Vector(1, 3), new Vector(10, 1));
                yield return (r1, r2, new Vector(11, 2));
                yield return (r1, r3, null);
            }
        }

        [TestCaseSource(nameof(RayPairsSource))]
        public void RaysCrossPoint((Ray r1, Ray r2, Vector expected) bundle)
        {
            var (r1, r2, expected) = bundle;
            var engine = new RayEngine();
            if (expected != null)
            {
                var success = engine.TryFindCrossingPoint(r1, r2, out Vector point);
                Assert.IsTrue(success && expected == point);
            }
            else
            {
                Assert.IsFalse(engine.TryFindCrossingPoint(r1, r2, out Vector point));
            }
        }
    }
}
