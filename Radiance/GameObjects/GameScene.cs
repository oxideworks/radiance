using Radiance.Primitives;
using Radiance.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public sealed class GameScene
    {
        public GameScene(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        private readonly IRenderer renderer;

        public void Tick()
        {
            var obs = new Obstacle(new[] { new Vector(1, 2), new Vector(3, 4) });
        }
    }
}
