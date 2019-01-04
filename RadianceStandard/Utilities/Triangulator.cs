using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Utilities
{
    public class Triangulator
    {
        #region Methods
        public Triangulation Triangulate(IHardenedPolymer polymer)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region privates
        private (Triangle C, Triangle D) Flip(Triangle A, Triangle B)
        {
            var mutual = A.Polymer.Intersect(B.Polymer).ToList();
            if (mutual.Count != 2)
                throw new Exception("This triangles don`t constitute quadrilateral!");
            var union = A.Polymer.Union(B.Polymer).ToList();

            Vector m0 = mutual[0];
            Vector m1 = mutual[1];
            var c = new Polymer(union.Except(new[] { m0 }));
            var d = new Polymer(union.Except(new[] { m1 }));
            var C = new Triangle(c);
            var D = new Triangle(d);

            var allNeighbours = A.Neighbours.Union(B.Neighbours).Except(new[] { A, B }).ToList();
            List<Triangle> findNeighbours(Func<Vector, bool> func)
                => allNeighbours.Where(n => n.Polymer.Any(func)).ToList();
            C.Neighbours = findNeighbours(p => p == m1);
            D.Neighbours = findNeighbours(p => p == m0);

            void handleNeighbours(Triangle @base, Triangle toAdd)
            {
                @base.Neighbours.Remove(A);
                @base.Neighbours.Remove(B);
                @base.Neighbours.Add(toAdd);
            }

            C.Neighbours.ForEach(x => handleNeighbours(x, C));
            D.Neighbours.ForEach(x => handleNeighbours(x, D));

            return (C, D);
        }

        private bool DelaunayCondition(Triangle triangle, Vector point)
        {
            return (triangle.Center - point).LengthSquared >= triangle.RadiusSquared;
        }

        private bool TryFlip(Triangle A, Triangle B, out (Triangle C, Triangle D) bundle)
        {
            bool tryFlip(Triangle a, Triangle b, out (Triangle C, Triangle D) pack)
            {
                foreach (var node in a.Polymer)
                {
                    if (!DelaunayCondition(b, node))
                    {
                        pack = Flip(a, b);
                        return true;
                    }
                }
                pack = (null, null);
                return false;
            }

            return tryFlip(A, B, out bundle) || tryFlip(B, A, out bundle);
        }
        #endregion
    }
}
