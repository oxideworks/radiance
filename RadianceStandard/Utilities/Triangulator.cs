using RadianceStandard.Primitives;
using System;
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
            var c = new Polymer(A.Polymer.Take(2)) { B.Polymer[1] };
            var d = new Polymer(B.Polymer.Skip(1)) { A.Polymer[0] };
            var C = new Triangle(c);
            var D = new Triangle(d);
            C.Neighbours.AddRange(new[] { A.Neighbours[0], B.Neighbours[1], D });
            D.Neighbours.AddRange(new[] { C, B.Neighbours[2], A.Neighbours[2] });
            return (C, D);
        }

        private bool DelaunayCondition(Triangle A, Triangle B)
        {
            throw new NotImplementedException();
        }

        private void TryFlip(Triangle A, Triangle B)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
