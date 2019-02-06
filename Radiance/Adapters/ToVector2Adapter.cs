using System.Numerics;

namespace Radiance.Adapters
{
    public class ToVector2Adapter
    {
        public ToVector2Adapter(RadianceStandard.Primitives.Vector vector)
        {
            vector2 = new Vector2(vector.X, vector.Y);
        }

        private Vector2 vector2;

        public static implicit operator Vector2(ToVector2Adapter adapter)
        {
            return adapter.vector2;
        }
    }
}
