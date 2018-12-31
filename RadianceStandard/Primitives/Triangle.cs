using RadianceStandard.Exceptions;
using System.Collections.Generic;

namespace RadianceStandard.Primitives
{
    public class Triangle
    {
        #region Ctors
        public Triangle(Vector a, Vector b, Vector c, List<Triangle> neighbours)
            : this(new Polymer(new[] { a, b, c }), neighbours)
        {

        }

        public Triangle(IHardenedPolymer polymer, List<Triangle> neighbours)
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
