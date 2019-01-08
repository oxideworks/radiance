using RadianceStandard.GameObjects;
using RadianceStandard.IInput;
using RadianceStandard.IRender;
using RadianceStandard.Primitives;
using RadianceStandard.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Radiance.GameObjects
{
    public sealed class GameScene
    {
        #region Ctors
        public GameScene(IDynamicRenderer renderer, IKeyboardInput keyboardInput, IMouseInput mouseInput)
        {
            this.renderer = renderer;
            renderer.Tick += (s, e) => Tick();

            this.keyboardInput = keyboardInput;
            this.mouseInput = mouseInput;
            this.mouseInput.OnMouseMoved += (s, e) => lastMousePosition = e;
            lastMousePosition = this.mouseInput.MousePosition;

            obstacles = GenerateObstacles();
            TestTriangulation();
        }
        #endregion

        #region fields
        private readonly IDynamicRenderer renderer;
        private readonly IKeyboardInput keyboardInput;
        private readonly IMouseInput mouseInput;
        private readonly List<IObstacle> obstacles;
        private Vector lastMousePosition;
        #endregion

        #region Props

        #endregion

        #region Methods
        public void Tick()
        {
            renderer.RenderObstacles(obstacles);
            FindHulls(obstacles).ForEach(renderer.RenderPoints);
            renderer.RenderPoints(new[] { lastMousePosition });
            renderer.RenderText(lastMousePosition.ToString(), lastMousePosition);
        }
        #endregion

        #region privates
        private void TestTriangulation()
        {
            var trianglulation = new Triangulation(obstacles[1].Polymer, renderer);
        }

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
                new Vector(235, 180),
                new Vector(300),
                new Vector(180, 270),
                new Vector(60, 300)
                })),

                new Obstacle(new Polymer(new[] {
                new Vector(700),
                new Vector(200, 700),
                new Vector(530),
                new Vector(700, 200)
                }))
            };
            return obs;
        }

        private readonly ConvexHuller huller = new ConvexHuller();
        private IHardenedPolymer FindHull(IObstacle obstacle)
        {
            return huller.ComputeConvexHull(obstacle.Polymer);
        }

        private List<IHardenedPolymer> FindHulls(List<IObstacle> obstacles)
        {
            return obstacles.Select(FindHull).ToList();
        }
        #endregion
    }
}
