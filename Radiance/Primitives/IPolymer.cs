using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    public interface IPolymer : IEnumerable<Vector>
    {
        Vector this[int index] { get; }
        int Length { get; }
    }
}
