using Radiance.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects
{
    public interface IObstacle
    {
        IHardenedPolymer Polymer { get; }
        bool Contains(Vector point);
        bool Intersects(IObstacle obstacle);
    }
}
