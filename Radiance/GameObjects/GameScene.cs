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
            obstacles = GenerateObstacles();
        }

        private readonly IRenderer renderer;
        private readonly List<IObstacle> obstacles;

        private List<IObstacle> GenerateObstacles()
        {
            var obs = new List<IObstacle>
            {
                new Obstacle(new Polymer(new[] {
                new Vector(10),
                new Vector(200, 10),
                new Vector(10,200)
                })),

                new Obstacle(new Polymer(new[] {
                new Vector(60),
                new Vector(300,60),
                new Vector(300),
                new Vector(60,300)
                }))
            };
            return obs;
        }

        public void Tick()
        {
            renderer.RenderObstacles(obstacles);
        }
    }
}
