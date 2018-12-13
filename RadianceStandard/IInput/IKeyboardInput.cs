using System;
using Windows.System;

namespace RadianceStandard.IInput
{
    public interface IKeyboardInput
    {
        event EventHandler<VirtualKey> OnKeyDown;
        event EventHandler<VirtualKey> OnKeyUp;
    }
}
