using RadianceStandard.Exceptions;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.Primitives
{
    public class Segment
    {
        #region Ctors
        public Segment(Vector a, Vector b) : this(new Polymer(new[] { a, b }))
        {

        }

        public Segment(Polymer polymer)
        {
            if (polymer.Count != 2) throw new InvalidNumberOfNodesException("Чето тут не так.");
            this.polymer = polymer;
        }
        #endregion

        #region Properties
        public Vector A { get => polymer[0]; }
        public Vector B { get => polymer[1]; }
        public IHardenedPolymer Polymer { get => polymer; }
        #endregion

        #region Methods
        public Ray ToRay()
        {
            return new Ray(A, B);
        }

        public bool TryFindCrossingPoint(Segment segment, out Vector point)
        {
            var engine = new RayEngine();
            var thisRay = ToRay();
            var p = engine.FindCrossingParams(thisRay, segment.ToRay());
            if (p.HasValue)
            {
                var (t1, t2) = (p.Value.t1, p.Value.t2);
                if (t1 >= 0 && t2 >= 0 && t2 <= 1)
                {
                    point = thisRay.Origin + thisRay.Direction * t1;
                    return true;
                }
            }
            point = null;
            return false;
        }
        #endregion

        #region privates
        private readonly Polymer polymer;
        #endregion 
    }
}
