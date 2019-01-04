using NUnit.Framework;
using RadianceStandard.Primitives;

namespace TestsCore.PrimitivesTests
{
    [TestFixture]
    public class MatrixTests
    {
        #region Determinant Tests
        [TestCase]
        public void TestZeroDeterminant()
        {
            var matrix = Matrix.Randomize(new Matrix.MatrixSize(15));
            var i = 5;
            for (int j = 0; j < 15; j++)
                matrix[i, j] = matrix[i + 1, j];
            Assert.AreEqual(0, matrix.Determinant);
        }

        [TestCase]
        public void TestDeterminant()
        {
            var lines = new[] {
                "1 2 3",
                "6 10 5",
                "3 18 11"
            };
            var matrix = Matrix.FromLines(lines);
            Assert.AreEqual(152, matrix.Determinant);
        }
        #endregion
    }
}
