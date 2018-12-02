using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    public class Polymer : IPolymer
    {
        public Polymer(List<Vector> points)
        {
            this.points = points;
        }

        public Polymer(Vector[] points) : this(points.ToList())
        {

        }

        private readonly List<Vector> points;

        public Vector this[int index] => points[index];
        public int Length { get => points.Count; }

        public IEnumerator<Vector> GetEnumerator()
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
