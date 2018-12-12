using System.Collections.Generic;
using System.Numerics;

namespace RadianceStandard.Primitives
{
    public interface IHardenedPolymer : IReadOnlyList<Vector>
    {

    }

    public class Polymer : List<Vector>, IHardenedPolymer
    {
        public Polymer()
        {

        }

        public Polymer(IEnumerable<Vector> nodes) : base(nodes)
        {

        }

        public Polymer(Polymer polymer) : this((IEnumerable<Vector>)polymer)
        {

        }
    }
}
