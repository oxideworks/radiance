using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    public interface IObstacleFactory
    {
        IObstacle CreateObstacle();
        IObstacle[] CreateObstacles(int count);
    }

    public class GridObstacleFactory : IObstacleFactory
    {
        public GridObstacleFactory(Rect bounds)
        {
            this.bounds = bounds;
        }

        private readonly Rect bounds;
        private int nextX = 40;
        private int nextY = 60;
        private readonly Random rnd = new Random();
        public IObstacle CreateObstacle()
        {
            var width = 40;
            var height = 40;
            var points = new Vector2[4];
            points[0] = new Vector2(nextX, nextY);
            points[1] = new Vector2(nextX + width, nextY);
            points[2] = new Vector2(nextX + width, nextY + height);
            points[3] = new Vector2(nextX, nextY + height);
            var obs = new Obstacle(points);
            nextX += rnd.Next(3, 7) * width;
            if (nextX + width > bounds.Width)
            {
                nextX = 0;
                nextY += rnd.Next(3, 7) * height;
            }
            return obs;
        }

        public IObstacle[] CreateObstacles(int count)
        {
            var obs = new IObstacle[count];
            for (int i = 0; i < count; i++)
                obs[i] = CreateObstacle();
            return obs;
        }
    }

    public class RandomObstacleFactory : IObstacleFactory
    {
        private RandomObstacleFactory()
        {

        }

        public RandomObstacleFactory(Rect bounds)
        {
            this.bounds = bounds;
        }

        private readonly Rect bounds;
        private Random rnd = new Random();

        public IObstacle CreateObstacle()
        {
            var maxShift = 600;
            var vxCount = rnd.Next(3, 5);
            var points = new List<Vector2>();
            var shiftX = rnd.Next(maxShift);
            var shiftY = rnd.Next(maxShift);
            for (int i = 0; i < vxCount; i++)
            {
                var p = new Vector2((float)rnd.NextDouble() * bounds.Width / 5, (float)rnd.NextDouble() * bounds.Height / 8);
                p.X += shiftX;
                p.Y += shiftY;
                if (points.Count > 2)
                    if (new Obstacle(points.ToArray()).Contains(p))
                    {
                        i--;
                        continue;
                    }
                points.Add(p);
            }
            var sorter = new PointsSorter();
            var sorted = points.ToArray();//sorter.Sort(points.ToArray());
            return new Obstacle(sorted);
        }

        public IObstacle[] CreateObstacles(int count)
        {
            var obs = new List<IObstacle>();
            for (int i = 0; i < count; i++)
            {
                var newobs = CreateObstacle();
                if (obs.Any(x => newobs.Intersects(x))) i--;
                else obs.Add(newobs);
            }
            return obs.ToArray();
        }

    }

}
