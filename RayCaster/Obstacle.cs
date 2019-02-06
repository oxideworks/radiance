using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MIConvexHull;

namespace RayCaster
{
    public interface IObstacle
    {
        bool Contains(Vector2 point);
        bool Intersects(IObstacle obstacle);
        List<(float x1, float y1, float x2, float y2)> ChopSegments();
        Vector2[] Points { get; }
    }

    public class Obstacle : IObstacle
    {
        private Obstacle()
        {

        }

        public Obstacle(Vector2[] points)
        {
            Points = points;
        }

        public Vector2[] Points { get; private set; }

        public List<(float x1, float y1, float x2, float y2)> ChopSegments()
        {
            var segments = new List<(float, float, float, float)>();
            for (int i = 0; i < Points.Count() - 1; i++)
                segments.Add((Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y));
            segments.Add((Points.Last().X, Points.Last().Y, Points[0].X, Points[0].Y));
            return segments;
        }

        //private ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>> hull;
        public bool Contains(Vector2 point)
        {
            List<double[]> data = PrepareData(Points, point);
            var hull = ConvexHull.Create(data);
            var hullPoints = hull.Points.Select(x => x.Position).ToList();
            var arrayPoint = Point2ToArray(point);
            var onHull = false;
            foreach (var p in hullPoints)
                if (Enumerable.SequenceEqual(p, arrayPoint))
                    onHull = true;
            if (onHull) return false;
            else return true;
        }

        private List<double[]> PrepareData(Vector2[] points, Vector2 sidePoint)
        {
            var result = new List<double[]>();
            foreach (var point in points)
                result.Add(Point2ToArray(point));
            result.Add(Point2ToArray(sidePoint));
            return result;
        }

        private static double[] Point2ToArray(Vector2 point)
        {
            return new double[] { point.X, point.Y };
        }

        public bool Intersects(IObstacle obstacle)
        {
            foreach (var p in Points)
                if (obstacle.Contains(p)) return true;
            return false;
        }
    }
}
