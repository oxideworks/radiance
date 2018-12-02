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
        public Obstacle(List<Vector> points) : base(Sort(points))
        {

        }

        private static List<Vector> Sort(List<Vector> points)
        {
            // Do sorting here.
            return points;
        }
    }
}
