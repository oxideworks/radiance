using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    public class Polymer : IEnumerable<Vector2d>
    {
        public Polymer(List<Vector2d> points)
        {
            this.points = points;
        }

        public Polymer(Vector2d[] points) : this(points.ToList())
        {

        }

        private readonly List<Vector2d> points;

        public Vector2d this[int index] => points[index];
        public int Length { get => points.Count; }

        public IEnumerator<Vector2d> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
