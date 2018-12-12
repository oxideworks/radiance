using Radiance.Render;
using Radiance.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Radiance
{
    public sealed partial class GamePage : Page
    {
        private readonly Renderer renderer;
        private readonly GameScene scene;
        public GamePage()
        {
            InitializeComponent();
            renderer = new Renderer(canvas);
            scene = new GameScene(renderer);
            canvas.Draw += (s, e) => scene.Tick();
        }
    }
}
