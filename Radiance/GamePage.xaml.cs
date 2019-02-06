using Radiance.GameObjects;
using Radiance.Input;
using Radiance.Render;
using Windows.UI.Xaml.Controls;

namespace Radiance
{
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
            new GameScene(new DynamicRenderer(canvas), new KeyboardInput(), new MouseInput());
        }
    }
}
