using RadianceStandard.Exceptions;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Primitives
{
    public class Triangle
    {
        #region Ctors
        public Triangle(Vector a, Vector b, Vector c)
           : this(a, b, c, new List<Triangle>())
        {

        }

        public Triangle(Vector a, Vector b, Vector c, IEnumerable<Triangle> neighbours)
            : this(new Polymer(new[] { a, b, c }), neighbours)
        {

        }

        public Triangle(IHardenedPolymer polymer)
           : this(polymer, new List<Triangle>())
        {

        }

        public Triangle(IHardenedPolymer polymer, IEnumerable<Triangle> neighbours)
            : this(polymer, neighbours.ToList())
        {

        }

        private Triangle(IHardenedPolymer polymer, List<Triangle> neighbours)
        {
            if (polymer.Count != 3 || neighbours.Count > 3)
                throw new InvalidNumberOfNodesException("Чето тут не так.");
            Polymer = polymer;
            Neighbours = neighbours;
        }
        #endregion

        #region fields
        private Vector center;
        private float? radiusSquared;
        #endregion

        #region Props
        public IHardenedPolymer Polymer { get; set; }
        public List<Triangle> Neighbours { get; set; }

        public Vector CircleCenter
        {
            get
            {
                if (center == null)
                    center = ComputeCenter();
                return center;
            }
        }

        public float CircleRadiusSquared
        {
            get
            {
                if (!radiusSquared.HasValue)
                    radiusSquared = ComputeRadius();
                return radiusSquared.Value;
            }
        }

        public float CircleRadius => (float)Math.Sqrt(CircleRadiusSquared);
        #endregion

        #region Methods
        public Triangle FindFriend(Vector point)
        {
            var rayEngine = new RayEngine();
            Segment segment = null;
            var ray = new Ray(CircleCenter, point);
            foreach (var edge in Polymer.ToSegments())
            {
                if (rayEngine.TryFindCrossingPoint(ray, edge, out Vector foo))
                {
                    segment = edge;
                    break;
                }
            }
            if (segment != null)
            {
                var triangle = Neighbours.First(n => n.Polymer.ToSegments().Any(y => y == segment));
                if (triangle.Polymer.ContainsPoint(point))
                    return triangle;
                else
                    return triangle.FindFriend(point);
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return string.Join("; ", Polymer.Select(x => x.ToString()));
        }
        #endregion

        #region privates
        //private double a, b, c, d;
        private Vector ComputeCenter()
        {
            var list = new List<List<double>> {
                new List<double> (Enumerable.Repeat<double>(0, 4)),
                new List<double> { Polymer[0].LengthSquared, Polymer[0].X, Polymer[0].Y, 1 },
                new List<double> { Polymer[1].LengthSquared, Polymer[1].X, Polymer[1].Y, 1 },
                new List<double> { Polymer[2].LengthSquared, Polymer[2].X, Polymer[2].Y, 1 },
            };
            Matrix matrix = Matrix.FromList(list);
            var a = matrix.CutMinor(0, 0).Determinant.Value;
            var b = matrix.CutMinor(0, 1).Determinant.Value;
            var c = matrix.CutMinor(0, 2).Determinant.Value;
            //var d = matrix.CutMinor(0, 3).Determinant.Value;

            return new Vector((float)(b / (2 * a)), (float)(-1 * c / (2 * a)));
        }

        private float ComputeRadius()
        {
            if (CircleCenter == null)
            {
                ComputeCenter();
            }
            //return (float)((b * b + c * c - 4 * a * d) / (4 * a * a));


            //#warning Может можно красивеe
            //return Polymer.Select(x => (CircleCenter - x).LengthSquared).Aggregate((acc, x) => acc += x) / 3;

            return (CircleCenter - Polymer[0]).LengthSquared;
        }
        #endregion
    }
}
