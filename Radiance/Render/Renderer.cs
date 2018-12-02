using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
