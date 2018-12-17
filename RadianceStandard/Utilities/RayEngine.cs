using RadianceStandard.Primitives;

namespace RadianceStandard.Utilities
{
    public class RayEngine
    {
        public bool TryFindCrossingPoint(Ray r1, Ray r2, out Vector point)
        {
            var p = FindCrossingParams(r1, r2);
            // (t1, t2 > 0) its ray, not line
            if (p.HasValue && p.Value.t1 > 0 && p.Value.t2 > 0)
            {
                point = r1.Origin + r1.Direction * p.Value.t1;
                return true;
            }
            else
            {
                point = null;
                return false;
            }
        }

        public (float t1, float t2)? FindCrossingParams(Ray r1, Ray r2)
        {
            var (ax, ay) = r1.Direction.ToTuple();
            var (bx, by) = r2.Direction.ToTuple();
            var (dx, dy) = (r1.Origin - r2.Origin).ToTuple();
            var q = ax * by - ay * bx;
            if (q == 0) return null;
            var t1 = (bx * dy - by * dx) / q;
            var t2 = (ax * dy - ay * dx) / q;
            return (t1, t2);
        }
    }
}
