using RadianceStandard.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.IRender
{
    public interface IRenderer
    {
        void RenderObstacles(IEnumerable<IObstacle> obstacles);
    }
}
