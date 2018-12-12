using Radiance.Render;
using RadianceStandard.GameObjects;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("==================================================================");
            Debug.WriteLine(obstacles[0].Intersects(obstacles[1]));
            Debug.WriteLine(obstacles[1].Intersects(obstacles[0]));
            Debug.WriteLine("==================================================================");
            Debug.WriteLine(obstacles[1].Intersects(obstacles[2]));
            Debug.WriteLine(obstacles[2].Intersects(obstacles[1]));
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
                new Vector(10, 200)
                })),

                new Obstacle(new Polymer(new[] {
                new Vector(60),
                new Vector(300, 60),
                new Vector(300),
                new Vector(60, 300)
                })),

                new Obstacle(new Polymer(new[] {
                new Vector(700),
                new Vector(200, 700),
                new Vector(700, 200)
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
