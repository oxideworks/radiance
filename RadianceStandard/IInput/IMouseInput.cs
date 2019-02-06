using RadianceStandard.Primitives;
using System;

namespace RadianceStandard.IInput
{
    public interface IMouseInput
    {
        Vector MousePosition { get; }
        event EventHandler<Vector> OnMouseMoved;
    }
}
