using Radiance.Primitives;
using Radiance.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public class Obstacle : IObstacle
    {
        public Obstacle(Polymer polymer)
        {
            if (polymer.Count < 3) throw new Exception("Орсен против вырожденных полимеров!");
            var sorter = new PolymerSorter();
            //Polymer = sorter.Sort(polymer, );
        }

        public IHardenedPolymer Polymer { get; private set; }

        public bool Contains(Vector point)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(IObstacle obstacle)
        {
            throw new NotImplementedException();
        }
    }
}
