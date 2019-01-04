using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RadianceStandard.Primitives
{
    public class Matrix
    {
        public struct MatrixSize
        {
            public MatrixSize(int height, int width)
            {
                Height = height;
                Width = width;
            }

            public MatrixSize(int a)
            {
                Height = a;
                Width = a;
            }

            public int Height { get; private set; }
            public int Width { get; private set; }
            public int Max { get => Math.Max(Height, Width); }

            public MatrixSize Deform(int heightShift, int widthShift)
            {
                return new MatrixSize(Height + heightShift, Width + widthShift);
            }
        }

        private Matrix() { }
        public Matrix(MatrixSize size) : this(size.Height, size.Width) { }
        public Matrix(int n, int m)
        {
            matrix = new double[n, m];
            Size = new MatrixSize(n, m);
        }

        public double this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        private readonly double[,] matrix;
        public MatrixSize Size { get; private set; }
        public bool IsSquare { get => Size.Height == Size.Width; }

        public double? Determinant
        {
            get
            {
                if (!IsSquare) return null;
                var proxy = Copy();
                var sign = 1;
                for (int j = 0; j < Size.Width - 1; j++)
                {
                    var a0 = proxy[j, j];
                    if (a0 == 0)
                    {
                        int row = j + 1;
                        while (proxy[row, j] == 0)
                            row++;
                        for (int col = j; col < Size.Width; col++)
                        {
                            var t = proxy[j, col];
                            proxy[j, col] = proxy[row, col];
                            proxy[row, col] = t;
                        }
                        sign *= -1;
                        a0 = proxy[j, j];
                    }
                    for (int i = j + 1; i < Size.Height; i++)
                    {
                        var k = proxy[i, j] / a0;
                        for (int c = j; c < Size.Width; c++)
                            proxy[i, c] = proxy[i, c] - proxy[j, c] * k;
                    }
                }

                var det = (double)sign;
                for (int i = 0; i < Size.Max; i++)
                    det *= proxy[i, i];
                return det;
            }
        }

        public Matrix Replace(Matrix with, int iCorner, int jCorner)
        {
            var proxy = Copy();
            for (int i = 0; i < with.Size.Height; i++)
                for (int j = 0; j < with.Size.Width; j++)
                    proxy[i + iCorner, j + jCorner] = with[i, j];
            return proxy;
        }

        public Matrix Copy()
        {
            var cpy = new Matrix(Size);
            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < Size.Width; j++)
                    cpy[i, j] = this[i, j];
            return cpy;
        }

        public Matrix CutMinor(int y, int x)
        {
            var minor = new Matrix(Size.Deform(-1, -1));
            var ish = 0;
            for (int i = 0; i < minor.Size.Height; i++)
            {
                if (i == y) ish = 1;
                var jsh = 0;
                for (int j = 0; j < minor.Size.Width; j++)
                {
                    if (j == x) jsh = 1;
                    minor[i, j] = this[i + ish, j + jsh];
                }
            }
            return minor;
        }

        public override string ToString()
        {
            var @string = string.Empty;
            for (int i = 0; i < Size.Height; i++)
            {
                for (int j = 0; j < Size.Width; j++) @string += $"{Math.Round(this[i, j], 2)}\t";
                @string = @string.Trim() + "\n";
            }
            return @string.Trim();
        }

        static public Matrix FromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var matrix = FromLines(lines);
            return matrix;
        }

        static public Matrix FromLines(IEnumerable<string> lines)
        {
            var n = lines.Where(x => x.Trim() != string.Empty).Count();
            var m = lines.Select(x => x.Split(' ').Length).Aggregate((max, x) => max = Math.Max(max, x));
            var matrix = new Matrix(n, m);
            var i = 0;
            foreach (var line in lines)
            {
                var vals = line.Split(' ').Select((x) => Convert.ToDouble(x)).ToList();
                var j = 0;
                foreach (var val in vals)
                {
                    matrix[i, j] = val;
                    j++;
                }
                i++;
            }
            return matrix;
        }

        static public Matrix Randomize(MatrixSize size)
        {
            var m = new Matrix(size);
            var rnd = new Random();
            for (int i = 0; i < size.Height; i++)
                for (int j = 0; j < size.Width; j++)
                    m[i, j] = rnd.Next(-200, 200);
            return m;
        }

        public (Matrix L, Matrix U) ParseToLU()
        {
            var U = new Matrix(Size);
            var L = new Matrix(Size);

            for (int j = 0; j < Size.Max; j++)
            {
                U[0, j] = this[0, j];
                L[j, 0] = this[j, 0] / U[0, 0];
            }

            for (int i = 1; i < Size.Max; i++)
                for (int j = i; j < Size.Max; j++)
                {
                    U[i, j] = this[i, j];
                    for (int k = 0; k < i; k++)
                        U[i, j] -= L[i, k] * U[k, j];
                    L[j, i] = this[j, i];
                    for (int k = 0; k < i; k++)
                        L[j, i] -= L[j, k] * U[k, i];
                    L[j, i] /= U[i, i];
                }

            return (L, U);
        }

    }

}
