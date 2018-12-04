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

        }
    }
}
