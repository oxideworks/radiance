using NUnit.Framework;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestsCore
{
    [TestFixture]
    public class TestingField
    {
        [TestCase]
        public void Test()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            foreach (var item in list.ToList())
            {
                list.Remove(item);
            }

            Assert.AreEqual(new List<int>(), list);
        }

        [TestCase]
        public void Test2()
        {
            Func<IEnumerable<Vector>> func = () => { return new List<Vector>(); };
            IEnumerable<Vector> func2() { return new List<Vector>(); }
            func2();

            Assert.Pass();
        }

        [TestCase]
        public void Test3()
        {
            var circle = new List<Vector>();
            var count = 12;
            var center = new Vector(340, 200);
            var line = new Vector(0, 110);
            var df = 2 * Math.PI / count;
            for (int i = 0; i < count; i++)
                circle.Add(center + line.Turn(df * i));
            var xs = $"[{string.Join(", ", circle.Select(c => c.X.ToString()))}]";
            var ys = $"[{string.Join(", ", circle.Select(c => c.Y.ToString()))}]";
        }

        [TestCase]
        public void Test4()
        {
            var X = new List<float>();
            var Y = new List<float>();
            for (int x = 550; x <= 850; x += 20)
            {
                var kek = (float)(1 - (x - 695) * (x - 695) / 22500.0) * 8100;
                X.Add(x);
                var y1 = (float)Math.Sqrt(Math.Abs(kek)) + 171;
                Y.Add(y1);
                X.Add(x);
                var y2 = (float)(-1 * Math.Sqrt(Math.Abs(kek)) + 171);
                Y.Add(y2);
            }

            string strX = string.Join(", ", X.Select(px => px.ToString()));
            string strY = string.Join("f, ", Y.Select(py => py.ToString()));

            Assert.Pass();
        }
    }
}
