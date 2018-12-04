using System.Collections.Generic;

namespace Radiance.Primitives
{
    public interface IStonedPolymer : IReadOnlyList<Vector>
    {

    }

    public class Polymer : List<Vector>, IStonedPolymer
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
