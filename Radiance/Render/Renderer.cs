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
        }

        private readonly CanvasAnimatedControl canvas;
    }
}
