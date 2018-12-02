using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Radiance.Input
{
    public class MouseInput
    {
        public MouseInput()
        {
            Window.Current.CoreWindow.PointerMoved += MouseMoved;
        }

        private void MouseMoved(CoreWindow sender, PointerEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
