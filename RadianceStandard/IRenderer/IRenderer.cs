using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System.Collections.Generic;

namespace RadianceStandard.IRender
{
    public interface IRenderer
    {
        void RenderObstacles(IEnumerable<IObstacle> obstacles);
        void RenderSegments(IEnumerable<Segment> segments);
        void RenderPoints(IEnumerable<Vector> points);
        void RenderText(string text, Vector point);
    }
}
