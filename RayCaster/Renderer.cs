using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace RayCaster
{
    public interface IRenderer
    {
        void RenderCaster(Caster caster);
        void RenderObstacles(IObstacle[] obstacles);
        void RenderRays(Vector2 origin, Vector2[] rays);
        void RenderDots(Vector2[] dots);
        void RenderLight(Vector2[] points);
    }

    public class Renderer : IRenderer
    {
        private Renderer()
        {

        }

        public Renderer(CanvasAnimatedControl canvas)
        {
            this.canvas = canvas;
            this.canvas.Draw += (s, e) => session = e.DrawingSession;
        }

        readonly CanvasAnimatedControl canvas;
        CanvasDrawingSession session;

        public void RenderRays(Vector2 origin, Vector2[] rays)
        {
            foreach (var ray in rays)
                session.DrawLine(origin, ray + origin, Colors.Yellow, 1);
        }

        public void RenderCaster(Caster caster)
        {
            var radius = 10;
            session.DrawLine(caster.Position, caster.Position + caster.LookDirection * 250, Colors.DarkOrange, 1);
            session.FillCircle(caster.Position, radius, Colors.OrangeRed);
        }

        public void RenderObstacles(IObstacle[] obstacles)
        {
            foreach (var obs in obstacles)
            {
                var geom = CanvasGeometry.CreatePolygon(canvas, obs.Points);
                var color = HexToColor("#ff032939");
                session.DrawGeometry(geom, color, 3, new CanvasStrokeStyle { LineJoin = CanvasLineJoin.Round });
                //session.FillGeometry(geom, color);
            }
        }

        public void RenderDots(Vector2[] dots)
        {
            var counter = 0;
            foreach (var dot in dots)
            {
                session.DrawCircle(dot, 3, Colors.White);
                session.DrawText(counter.ToString(), dot, Colors.OrangeRed);
                counter++;
            }
        }

        public void RenderLight(Vector2[] points)
        {
            var geom = CanvasGeometry.CreatePolygon(canvas, points);
            session.FillGeometry(geom, HexToColor("#22faefa3"));
        }

        private Color HexToColor(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)Convert.ToUInt32(hex.Substring(0, 2), 16);
            byte r = (byte)Convert.ToUInt32(hex.Substring(2, 2), 16);
            byte g = (byte)Convert.ToUInt32(hex.Substring(4, 2), 16);
            byte b = (byte)Convert.ToUInt32(hex.Substring(6, 2), 16);
            var color = Color.FromArgb(a, r, g, b);
            return color;
        }
    }
}
