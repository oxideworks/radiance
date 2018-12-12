using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace RadianceStandard.GameObjects
{
    public interface IObstacle
    {
        IHardenedPolymer Polymer { get; }
        IReadOnlyList<Segment> Segments { get; }
        bool Contains(Vector point);
        bool Intersects(IObstacle obstacle);
    }
}
