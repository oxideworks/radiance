using Radiance.GameObjects;
using Radiance.Input;
using Radiance.Render;
using Windows.UI.Xaml.Controls;

namespace Radiance
{
    public sealed partial class GamePage : Page
    {
        private readonly GameScene scene;
        public GamePage()
        {
            InitializeComponent();
            scene = new GameScene(new Renderer(canvas), new KeyboardInput(), new MouseInput());
            //canvas.Draw += (s, e) => scene.Tick();
        }
    }
}
