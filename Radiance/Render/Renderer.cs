using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Radiance.Adapters;
using RadianceStandard.GameObjects;
using RadianceStandard.IRender;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;

namespace Radiance.Render
{
    public class Renderer : IDynamicRenderer
    {
        public Renderer(CanvasAnimatedControl canvas)
        {
            this.canvas = canvas;
            this.canvas.Draw += (s, e) => session = e.DrawingSession;
            this.canvas.Draw += (s, e) => Tick?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Tick;

        private readonly CanvasAnimatedControl canvas;
        private CanvasDrawingSession session;

        public void RenderObstacles(IEnumerable<IObstacle> obstacles)
        {
            foreach (var obs in obstacles)
                RenderObstacle(obs);
        }

        public void RenderObstacle(IObstacle obs)
        {
            var geom = CanvasGeometry.CreatePolygon(canvas, new ToVector2ListAdapter(obs));
            var color = HexToColor("#ffc7ecee");
            session?.DrawGeometry(
                geom,
                color,
                3,
                new CanvasStrokeStyle { LineJoin = CanvasLineJoin.Round }
            );
        }

        public void RenderSegments(IEnumerable<Segment> segments)
        {
            foreach (var segment in segments)
                RenderSegment(segment);
        }

        public void RenderSegment(Segment segment)
        {
            session?.DrawLine(
                new ToVector2Adapter(segment.A),
                new ToVector2Adapter(segment.B),
                HexToColor("#ff6ab04c")
            );
        }

        public void RenderPoints(IEnumerable<Vector> points)
        {
            foreach (var point in points.Take(1))
            {
                RenderPoint(point, "#ff7ed6df");
            }

            var counter = 0;
            foreach (var point in points.Skip(1))
            {
                counter++;
                RenderPoint(point);
                RenderText(counter.ToString(), point);
            }
        }

        public void RenderPoint(Vector point, string color = "#ff686de0")
        {
            session?.FillCircle(
                new ToVector2Adapter(point),
                3,
                HexToColor(color)
            );
        }

        public void RenderText(string text, Vector point)
        {
            session?.DrawText(text,
                new ToVector2Adapter(point),
                HexToColor("#ffdff9fb")
            );
        }

        private Color HexToColor(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            var a = (byte)Convert.ToUInt32(hex.Substring(0, 2), 16);
            var r = (byte)Convert.ToUInt32(hex.Substring(2, 2), 16);
            var g = (byte)Convert.ToUInt32(hex.Substring(4, 2), 16);
            var b = (byte)Convert.ToUInt32(hex.Substring(6, 2), 16);
            var color = Color.FromArgb(a, r, g, b);
            return color;
        }
    }
}
