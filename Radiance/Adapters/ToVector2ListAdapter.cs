using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System.Collections.Generic;
using System.Numerics;

namespace Radiance.Adapters
{
    public class ToVector2ListAdapter : List<Vector2>
    {
        public ToVector2ListAdapter(IObstacle obstacle)
            : this(obstacle.Polymer)
        {

        }

        public ToVector2ListAdapter(IHardenedPolymer polymer)
            : this((List<RadianceStandard.Primitives.Vector>)polymer)
        {

        }

        public ToVector2ListAdapter(List<RadianceStandard.Primitives.Vector> list)
            : base(list.ConvertAll(node => new Vector2(node.X, node.Y)))
        {

        }

        public static implicit operator Vector2[] (ToVector2ListAdapter adapter)
        {
            return adapter.ToArray();
        }
    }
}
