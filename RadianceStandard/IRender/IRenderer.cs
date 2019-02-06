using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;

namespace RadianceStandard.IRender
{
    public interface IRenderer
    {
        void RenderObstacles(IEnumerable<IObstacle> obstacles);
        void RenderSegments(IEnumerable<Segment> segments);
        void RenderSegments(IEnumerable<Segment> segments, string hexColor);
        void RenderPoints(IEnumerable<Vector> points);
        void RenderPoints(IEnumerable<Vector> points, string hexColor);
        void RenderText(string text, Vector point);
    }

    public interface IStaticRenderer : IRenderer
    {
        void Clear();
    }

    public interface IDynamicRenderer : IRenderer
    {
        event EventHandler Tick;
    }
}
