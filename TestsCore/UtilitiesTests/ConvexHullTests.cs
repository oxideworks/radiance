using NUnit.Framework;
using RadianceStandard.Primitives;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestsCore.UtilitiesTests
{
    [TestFixture]
    public class ConvexHullTests
    {
        #region IsValidConvexHull
        private static IEnumerable<(Polymer, Polymer)> IsValidConvexHullSource
        {
            get
            {
                List<float> listIn1 = new List<float>() { -1f, 0f, 1f, 0f, 0f, -1f, 0.75f, 3f, 2.5f, 2f, 3f, -0.25f, 2f, -2f,
                    -3f, -3f, -4f, -0.5f, -2f, 1f };
                List<float> listOut1 = new List<float>() { -3f, -3f, 2f, -2f, 3f, -0.25f, 2.5f, 2f,
                    0.75f, 3f, -2f, 1f, -4f, -0.5f };

                List<float> listIn2 = new List<float>() { 0f, 0f, 6f, 1f, 6f, 10f, 6f, 12f, 1.5f, 12f, -1f, 7f,
                    0f, 3f, 1f, 4f, 2f, 6f, 5f, 9f, 3f, 8f};
                List<float> listOut2 = new List<float>() { 0f, 0f, 6f, 1f, 6f, 10f, 6f, 12f, 1.5f, 12f, -1f, 7f };

                yield return (FillPolymerWithVectors(listIn1), FillPolymerWithVectors(listOut1));
                yield return (FillPolymerWithVectors(listIn2), FillPolymerWithVectors(listOut2));
            }
        }

        [TestCaseSource(nameof(IsValidConvexHullSource))]
        public void IsValidConvexHull((Polymer, Polymer) bundle)
        {
            var (actualPolymer, expectedPolymer) = bundle;
            ConvexHuller convexHuller = new ConvexHuller();
            IHardenedPolymer polymerRes = convexHuller.ComputeConvexHull(actualPolymer);

            if (polymerRes.Count == expectedPolymer.Count)
            {
                for (int i = 0; i < expectedPolymer.Count; i++)
                {
                    Assert.AreEqual(expectedPolymer[i].X, polymerRes[i].X);
                    Assert.AreEqual(expectedPolymer[i].Y, polymerRes[i].Y);
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        #endregion

        #region privates
        private static Polymer FillPolymerWithVectors(List<float> list)
        {
            var polymer = new Polymer();
            for (int i = 0; i < list.Count; i += 2)
            {
                polymer.Add(new Vector(list[i], list[i + 1]));
            }
            return polymer;
        }
        #endregion
    }
}
