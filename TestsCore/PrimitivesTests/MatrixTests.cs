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

        #region CutMinor Tests
        [TestCase]
        public void TestCutMinor00()
        {
            var lines = new[] {
                "1 2 3",
                "6 10 5",
                "3 18 11"
            };
            var expectedLines = new[] {
                "10 5",
                "18 11"
            };
            var matrix = Matrix.FromLines(lines);
            var actualMinor = matrix.CutMinor(0, 0);
            var expectedMinor = Matrix.FromLines(expectedLines);
            Assert.IsTrue(actualMinor.Equals(expectedMinor));
        }

        [TestCase]
        public void TestCutMinor11()
        {
            var lines = new[] {
                "1 2 3",
                "6 10 5",
                "3 18 11"
            };
            var expectedLines = new[] {
                "1 3",
                "3 11"
            };
            var matrix = Matrix.FromLines(lines);
            var actualMinor = matrix.CutMinor(1, 1);
            var expectedMinor = Matrix.FromLines(expectedLines);
            Assert.IsTrue(actualMinor.Equals(expectedMinor));
        }
        #endregion
    }
}
