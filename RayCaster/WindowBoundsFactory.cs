using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    public interface IWindowBoundsFactory
    {
        IObstacle CreateWindowBounds(Rect bounds);
    }

    public class WindowBoundsFactory : IWindowBoundsFactory
    {
        public IObstacle CreateWindowBounds(Rect bounds)
        {
            var scale = .95f;
            var points = new[] { scale * new Vector2(0),
                                 scale * new Vector2(bounds.Width, 0),
                                 scale * new Vector2(bounds.Width, bounds.Height),
                                 scale * new Vector2(0, bounds.Height)};

            return new Obstacle(points);
        }
    }
}
