using Radiance.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public class Obstacle : Polymer
    {
        public Obstacle(List<Vector2d> points) : base(Sort(points))
        {

        }

        private static List<Vector2d> Sort(List<Vector2d> points)
        {
            // Do sorting here.
            return points;
        }
    }
}
