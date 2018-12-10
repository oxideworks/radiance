using Radiance.GameObjects;
using Radiance.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Utilities
{
    public class ToVector2Adapter : List<Vector2>
    {
        public ToVector2Adapter(IObstacle obstacle)
            : this(obstacle.Polymer)
        {

        }

        public ToVector2Adapter(IHardenedPolymer polymer)
            : this((List<Primitives.Vector>)polymer)
        {

        }

        public ToVector2Adapter(List<Primitives.Vector> list)
            : base(list.ConvertAll(node => new Vector2(node.X, node.Y)))
        {

        }

        public static implicit operator Vector2[] (ToVector2Adapter adapter)
        {
            return adapter.ToArray();
        }
    }
}
