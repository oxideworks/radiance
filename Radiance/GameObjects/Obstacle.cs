using Radiance.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public class Obstacle : Polymer, IObstacle
    {
        public Obstacle(List<Vector> points)
            : base(Sort(points))
        {

        }

        public Obstacle(Vector[] points)
            : this(points.ToList())
        {

        }

        private static List<Vector> Sort(List<Vector> points)
        {
            var origin = points
                .OrderBy(p => p.Y)
                .ThenBy(p => p.X)
                .First();
            var ordered = points
                .OrderByDescending(p => Math.Atan2(p.Y - origin.Y, p.X - origin.X))
                .ToList();
            return ordered;
        }
    }
}
