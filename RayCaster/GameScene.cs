using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace RayCaster
{
    public class GameScene
    {
        private GameScene()
        {

        }

        public GameScene(IRenderer renderer)
        {
            this.renderer = renderer;
            HandleKeyboardInput();
            HandleMouseInput();

            caster = new Caster(40, 80);
            obstacles = CreateObstacles(15);
        }

        public readonly Caster caster;
        public readonly IObstacle[] obstacles;
        private readonly IRenderer renderer;

        public void Tick()
        {
            caster.Tick();
            PutRays();
            renderer.RenderObstacles(obstacles);
            renderer.RenderCaster(caster);
        }

        private IObstacle[] CreateObstacles(int obstaclesCount)
        {
            var obstacles = new List<IObstacle>();
            var bounds = new Rect(Window.Current.CoreWindow.Bounds);
            IObstacleFactory obstacleFactory = new GridObstacleFactory(bounds);//new RandomObstacleFactory(bounds);
            obstacles.AddRange(obstacleFactory.CreateObstacles(obstaclesCount));
            IWindowBoundsFactory boundsFactory = new WindowBoundsFactory();
            obstacles.Add(boundsFactory.CreateWindowBounds(bounds));
            return obstacles.ToArray();
        }

        private void PutRays()
        {
            Vector2 position = caster.Position;
            RenderLightFor(position);
            var shiftVector = new Vector2(3);
            var lights = 8;
            var fi = .0;
            var dfi = 2 * Math.PI / lights;
            for (int i = 0; i < lights; i++)
                RenderLightFor(position + TurnVector(shiftVector, fi + i * dfi));
        }

        private void RenderLightFor(Vector2 position)
        {
            Vector2[] lightPoints = FindLightPoints(position);
            //renderer.RenderRays(position, lightPoints.Select(x => x - position).ToArray());
            //renderer.RenderDots(lightPoints);
            renderer.RenderLight(lightPoints);
        }

        private Vector2 TurnVector(Vector2 vector, double angle)
        {
            var x = vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle);
            var y = vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle);
            return new Vector2((float)x, (float)y);
        }

        private Vector2[] FindLightPoints(Vector2 position)
        {
            var rays = new List<Vector2>();
            foreach (var obs in obstacles)
                foreach (var node in obs.Points)
                {
                    var relative = node - position;
                    var zeroDotProd = new Vector2(-relative.Y, relative.X);
                    var normalZeroDot = zeroDotProd / zeroDotProd.Length();
                    var scaledZeroDot = normalZeroDot / 2;// / (int)1e1;
                    rays.Add(relative);
                    rays.Add(relative + scaledZeroDot);
                    rays.Add(relative - scaledZeroDot);
                    //renderer.RenderDots(new[] { position + relative + scaledZeroDot, position + relative - scaledZeroDot });
                }
            //renderer.RenderRays(position, rays.ToArray());

            var segments = new List<(float x1, float y1, float x2, float y2)>();
            foreach (var obs in obstacles)
                segments.AddRange(obs.ChopSegments());

            var result = new List<Vector2>();
            foreach (var ray in rays)
            {
                var t1s = new List<double>();
                var R0x = position.X;
                var R0y = position.Y;
                var ax = (ray.X == 0) ? .005 : ray.X;
                var ay = ray.Y;
                foreach (var seg in segments)
                {
                    var S0x = seg.x1;
                    var S0y = seg.y1;
                    var bx = seg.x2 - seg.x1;
                    var by = seg.y2 - seg.y1;
                    if (ax * by == ay * bx) continue;
                    var t2 = (ax * (S0y - R0y) - ay * (S0x - R0x)) / (ay * bx - ax * by);
                    var t1 = (S0x - R0x + bx * t2) / ax;
                    //var eps = -1e-6;
                    var eps = 1e-6;
                    if (t1 < 0) continue;
                    if (t2 < -eps || t2 > 1 + eps) continue;
                    t1s.Add(t1);
                }
                if (t1s.Count < 1) continue;
                var mint1 = t1s.Min();
                result.Add(new Vector2((float)(R0x + ax * mint1), (float)(R0y + ay * mint1)));
            }
            var sorter = new PointsSorter();
            var sorted = sorter.Sort(result.ToArray(), position);//, caster.Position);
            return sorted;
        }

        private void HandleMouseInput()
        {
            Window.Current.CoreWindow.PointerMoved += MouseMoved;
        }

        private void HandleKeyboardInput()
        {
            Window.Current.CoreWindow.KeyDown += KeyDown;
            Window.Current.CoreWindow.KeyUp += KeyUp;
        }

        private void MouseMoved(CoreWindow sender, PointerEventArgs args)
        {
            var point = args.CurrentPoint.Position;
            var mpos = new Vector2((float)point.X, (float)point.Y);
            caster.OnMouseMoved(mpos);
        }

        private void KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            var key = args.VirtualKey;
            caster.OnKeyDown(key);
        }

        private void KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            var key = args.VirtualKey;
            caster.OnKeyUp(key);
        }
    }
}
