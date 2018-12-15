using RadianceStandard.GameObjects.Exceptions;
using RadianceStandard.Primitives;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.GameObjects
{
    public class Obstacle : IObstacle
    {
        public Obstacle(Polymer polymer)
        {
            if (polymer.Count < 3) throw new InvalidNumberOfNodesException("Орсен против вырожденных полимеров!");
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

        private CrossState Cross(Vector point, Segment segment)
        {
            var (x, y) = (point.X, point.Y);
            var (x1, y1) = (segment.A.X, segment.A.Y);
            var (x2, y2) = (segment.B.X, segment.B.Y);
            if (x1 == x2) return CrossState.False;
            var ycross = (x - x1) * (y2 - y1) / (x2 - x1) + y1;
            if (Math.Abs(ycross - y) < 1e-4) return CrossState.Break;
            if (x < Math.Min(x1, x2) || x > Math.Max(x1, x2)) return CrossState.False;
            if (ycross > y) return CrossState.False;
            return CrossState.True;
        }

        private enum CrossState : sbyte
        {
            False = 0,
            True = 1,
            Break = -1
        }

        public bool Contains(Vector point)
        {
            bool flag = false;
            foreach (var segment in segments)
            {
                CrossState state = Cross(point, segment);
                if (state == CrossState.Break) return true;
                flag ^= (state == CrossState.True) ? true : false;
            }
            return flag;
        }

        public bool Intersects(IObstacle obstacle)
        {
            foreach (var node in obstacle.Polymer)
                if (Contains(node)) return true;
            foreach (var node in Polymer)
                if (obstacle.Contains(node)) return true;
            return false;
        }
    }
}
