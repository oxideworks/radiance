using RadianceStandard.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.Primitives
{
    public class Segment
    {
        public Segment(Vector a, Vector b) : this(new Polymer(new[] { a, b }))
        {

        }

        public Segment(Polymer polymer)
        {
            if (polymer.Count != 2) throw new InvalidNumberOfNodesException("Чето тут не так.");
            this.polymer = polymer;
        }

        private readonly Polymer polymer;
        public Vector A { get => polymer[0]; }
        public Vector B { get => polymer[1]; }
        public IHardenedPolymer Polymer { get => polymer; }
    }
}
