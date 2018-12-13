using RadianceStandard.Primitives;
using System;

namespace RadianceStandard.IInput
{
    public interface IMouseInput
    {
        event EventHandler<Vector> OnMouseMoved;
    }
}
