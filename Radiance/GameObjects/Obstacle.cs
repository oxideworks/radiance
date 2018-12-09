using Radiance.Primitives;
using Radiance.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public class Obstacle : IObstacle
    {
        public Obstacle(Polymer polymer)
        {
            if (polymer.Count < 3) throw new Exception("Орсен против вырожденных полимеров!");
            this.polymer = polymer;
            segments = PairNodes();
        }

        private List<Segment> PairNodes()
        {
            var segments = new List<Segment>();
            for (int i = 0, j = Polymer.Count - 1; i < Polymer.Count; j = i++)
                segments.Add(new Segment(polymer[i], polymer[j]));
            return segments;
        }

        private readonly Polymer polymer;
        private readonly List<Segment> segments;
        public IHardenedPolymer Polymer { get => polymer; }
        public IReadOnlyList<Segment> Segments { get => segments; }

        private bool Cross(Vector point, Segment segment)
        {
            var (x, y) = (point.X, point.Y);
            var (x1, y1) = (segment.A.X, segment.A.Y);
            var (x2, y2) = (segment.B.X, segment.B.Y);
            if (x1 == x2) return false;
            if (x <= Math.Min(x1, x2) || x > Math.Max(x1, x2)) return false;
            var ycross = (x - x1) * (y2 - y1) / (x2 - x1) + y1;
            if (ycross > y) return false;
            return true;
        }

        public bool Contains(Vector point)
        {
            var flag = false;
            foreach (var segment in segments)
                flag ^= Cross(point, segment);
            return flag;
        }

        public bool Intersects(IObstacle obstacle)
        {
            throw new NotImplementedException();
        }
    }
}
