using System;
using Windows.System;
using Windows.UI.Xaml;

namespace Radiance.Input
{
    public class KeyboardInput
    {
        public KeyboardInput()
        {
            Window.Current.CoreWindow.KeyDown += (s, e) => OnKeyDown?.Invoke(s, e.VirtualKey); ;
            Window.Current.CoreWindow.KeyUp += (s, e) => OnKeyUp?.Invoke(s, e.VirtualKey);
        }

        public event EventHandler<VirtualKey> OnKeyDown;
        public event EventHandler<VirtualKey> OnKeyUp;
    }
}
