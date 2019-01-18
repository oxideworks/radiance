using NUnit.Framework;
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
    }
}
