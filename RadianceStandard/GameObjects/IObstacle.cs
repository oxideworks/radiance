using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace RadianceStandard.GameObjects
{
    public interface IObstacle
    {
        IHardenedPolymer Polymer { get; }
        IReadOnlyList<Segment> Segments { get; }
        bool Contains(Vector point);
        bool CompletelyContains(IObstacle obstacle);
        bool PartiallyContains(IObstacle obstacle);
        bool Intersects(IObstacle obstacle);
    }
}
