using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Radiance.Adapters;
using RadianceStandard.GameObjects;
using RadianceStandard.IRender;
using System;
using System.Collections.Generic;
using Windows.UI;

namespace Radiance.Render
{
    public class Renderer : IRenderer
    {
        public Renderer(CanvasAnimatedControl canvas)
        {
            this.canvas = canvas;
            this.canvas.Draw += (s, e) => session = e.DrawingSession;
        }

        private readonly CanvasAnimatedControl canvas;
        private CanvasDrawingSession session;

        public void RenderObstacles(IEnumerable<IObstacle> obstacles)
        {
            foreach (var obs in obstacles)
            {
                var geom = CanvasGeometry.CreatePolygon(canvas, new ToVector2Adapter(obs));
                var color = HexToColor("#ffc7ecee");
                session.DrawGeometry(
                    geom,
                    color,
                    3,
                    new CanvasStrokeStyle { LineJoin = CanvasLineJoin.Round }
                    );
            }
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
