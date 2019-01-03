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
            var renderer = new Renderer(canvas);
            var keyboardInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            scene = new GameScene(renderer, keyboardInput, mouseInput);
            canvas.Draw += (s, e) => scene.Tick();
        }
    }
}
