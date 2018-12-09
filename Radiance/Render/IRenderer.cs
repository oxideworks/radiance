using Radiance.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Render
{
    public interface IRenderer
    {
        void RenderObstacles(IEnumerable<IObstacle> obstacles);
    }
}
