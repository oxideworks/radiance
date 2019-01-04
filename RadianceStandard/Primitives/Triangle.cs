using RadianceStandard.Exceptions;
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

        #region Props
        public IHardenedPolymer Polymer;
        public List<Triangle> Neighbours;
        #endregion
    }
}
