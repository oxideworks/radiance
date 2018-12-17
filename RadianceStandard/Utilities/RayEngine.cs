using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadianceStandard.Utilities
{
    public class RayEngine
    {
        public bool TryFindCrossingPoint(Ray r1, Ray r2, out Vector point)
        {
            var p = FindCrossingParams(r1, r2);
            if (p.HasValue)
            {
                point = r1.Origin + r1.Direction * p.Value.t1;
                return true;
            }
            else
            {
                point = new Vector(0);
                return false;
            }
        }

        public (float t1, float t2)? FindCrossingParams(Ray r1, Ray r2)
        {
            var d = r2.Origin - r1.Origin;
            var (dx, dy) = (d.X, d.Y);
            var (ax, ay) = (r1.Origin.X, r1.Origin.Y);
            var (bx, by) = (r2.Origin.X, r2.Origin.Y);
            var px = ax * by;
            var py = ay * bx;
            var pdif = px - py;
            var psum = px + py;
            if (pdif == 0) return null;
            if (psum == 0) return null;
            var t1 = (by * dx - bx * dy) / pdif;
            var t2 = (ay * dx - ax * dy) / psum;
            return (t1, t2);
        }
    }
}
