using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;

namespace RadianceStandard.IRender
{
    public class StaticRenderer : IStaticRenderer
    {
        public StaticRenderer(IDynamicRenderer renderer)
        {
            this.renderer = renderer;
            this.renderer.Tick += ExecuteQueue;
            queue = new List<Action>();
        }

        private void ExecuteQueue(object sender, EventArgs e)
        {
            foreach (var action in queue)
                action.Invoke();
        }

        private readonly List<Action> queue;
        private readonly IDynamicRenderer renderer;

        public void Clear()
        {
            queue.Clear();
        }

        public void RenderObstacles(IEnumerable<IObstacle> obstacles)
        {
            queue.Add(() => renderer.RenderObstacles(obstacles));
        }

        public void RenderPoints(IEnumerable<Vector> points)
        {
            queue.Add(() => renderer.RenderPoints(points));
        }

        public void RenderPoints(IEnumerable<Vector> points, string hexColor)
        {
            queue.Add(() => renderer.RenderPoints(points, hexColor));
        }

        public void RenderSegments(IEnumerable<Segment> segments)
        {
            queue.Add(() => renderer.RenderSegments(segments));
        }

        public void RenderSegments(IEnumerable<Segment> segments, string hexColor)
        {
            queue.Add(() => renderer.RenderSegments(segments, hexColor));
        }

        public void RenderText(string text, Vector point)
        {
            queue.Add(() => renderer.RenderText(text, point));
        }
    }
}
