using RadianceStandard.Exceptions;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace RadianceStandard.GameObjects
{
    public class Obstacle : IObstacle
    {
        #region Ctors
        public Obstacle(Polymer polymer)
        {
            if (polymer.Count < 3) throw new InvalidNumberOfNodesException();
            this.polymer = polymer;
            segments = polymer.ToSegments();
        }
        #endregion

        #region Properties
        public IHardenedPolymer Polymer { get => polymer; }
        public IReadOnlyList<Segment> Segments { get => segments; }
        #endregion

        #region Methods
        public bool CompletelyContains(IObstacle obstacle)
        {
            foreach (var node in obstacle.Polymer)
                if (!Contains(node)) return false;
            return true;
        }

        public bool Contains(Vector point) => Polymer.ContainsPoint(point);

        public bool PartiallyContains(IObstacle obstacle)
        {
            if (Intersects(obstacle)) return true;
            if (CompletelyContains(obstacle)) return true;
            return false;
        }

        public bool Intersects(IObstacle obstacle)
        {
            foreach (var masterSegment in Segments)
                foreach (var visitorSegment in obstacle.Segments)
                    if (masterSegment.TryFindCrossingPoint(visitorSegment, out Vector _))
                        return true;
            return false;
        }
        #endregion

        #region privates
        private readonly Polymer polymer;
        private readonly List<Segment> segments;
        #endregion
    }
}
