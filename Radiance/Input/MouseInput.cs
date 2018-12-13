using RadianceStandard.IInput;
using RadianceStandard.Primitives;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Radiance.Input
{
    public class MouseInput : IMouseInput
    {
        public MouseInput()
        {
            Window.Current.CoreWindow.PointerMoved += MouseMoved;
        }

        private void MouseMoved(CoreWindow sender, PointerEventArgs args)
        {
            var pos = args.CurrentPoint.Position;
            var vect = new Vector((float)pos.X, (float)pos.Y);
            OnMouseMoved?.Invoke(sender, vect);
        }

        public event EventHandler<Vector> OnMouseMoved;
    }
}
