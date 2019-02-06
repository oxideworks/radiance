using RadianceStandard.GameObjects;
using RadianceStandard.IInput;
using RadianceStandard.IRender;
using RadianceStandard.Primitives;
using RadianceStandard.Utilities;
using System;
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
            //renderer.RenderObstacles(obstacles);
            FindHulls(obstacles).ForEach(renderer.RenderPoints);
            renderer.RenderPoints(new[] { lastMousePosition });
            renderer.RenderText(lastMousePosition.ToString(), lastMousePosition);
        }
        #endregion

        #region privates
        private void TestTriangulation()
        {
            new Triangulation(obstacles.Last().Polymer, renderer);

            new Triangulation(
                new Polymer {
                    new Vector(83, 545),
                    new Vector(149, 434),
                    new Vector(314, 413),
                    new Vector(580, 569),
                    new Vector(481, 814),
                    new Vector(178, 657),
                }, renderer
            );

            new Triangulation(
                new Polymer(
                    new Func<IEnumerable<Vector>>(() =>
                    {
                        var circle = new List<Vector>();
                        var count = 12;
                        var center = new Vector(340, 200);
                        var line = new Vector(0, 110);
                        var df = 2 * Math.PI / count;
                        for (int i = 0; i < count; i++)
                            circle.Add(center + line.Turn(df * i));
                        return circle;
                    }).Invoke()
                ), renderer);

            new Triangulation(
               new Polymer(
                   new Func<IEnumerable<Vector>>(() =>
                   {
                       var xs = new List<float> { 550, 550, 570, 570, 590, 590, 610, 610, 630, 630, 650, 650, 670, 670, 690, 690, 710, 710, 730, 730, 750, 750, 770, 770, 790, 790, 810, 810, 830, 830, 850, 850 };
                       var ys = new List<float> { 194.0434f, 147.9566f, 220.7494f, 121.2506f, 235.2729f, 106.7271f, 245.1552f, 96.84476f, 252.111f, 89.88896f, 256.8545f, 85.14547f, 259.7412f, 82.2588f, 260.95f, 81.05001f, 260.5489f, 81.45113f, 258.5157f, 83.48428f, 254.7317f, 87.26829f, 248.9423f, 93.05772f, 240.6491f, 101.3509f, 228.7841f, 113.2159f, 210.2301f, 131.7699f, 194.4308f, 147.5692f };
                       return Enumerable.Zip(xs, ys, (x, y) => new Vector(x, y));
                   }).Invoke()
               ), renderer);
        }

        private List<IObstacle> GenerateObstacles()
        {
            var obs = new List<IObstacle>
            {
                //new Obstacle(new Polymer(new[] {
                //new Vector(10),
                //new Vector(200, 10),
                //new Vector(10, 200)
                //})),

                //new Obstacle(new Polymer(new[] {
                //new Vector(60),
                //new Vector(300, 60),
                //new Vector(235, 180),
                //new Vector(300),
                //new Vector(180, 270),
                //new Vector(60, 300)
                //})),

                //new Obstacle(new Polymer(new[] {
                //new Vector(700),
                //new Vector(200, 700),
                //new Vector(530),
                //new Vector(700, 200)
                //}))

                new Obstacle(new Polymer(new[] {
                new Vector(1000, 250),
                new Vector(1350, 300),
                new Vector(900, 400),
                new Vector(1070, 550),
                new Vector(1400, 700)
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
