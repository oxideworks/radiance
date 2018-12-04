using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace RayCaster
{
    public sealed partial class MainPage : Page
    {
        private Renderer renderer;
        private GameScene scene;
        public MainPage()
        {
            InitializeComponent();
            canvas.ForceSoftwareRenderer = true;
            //canvas.IsFixedTimeStep = true;
            renderer = new Renderer(canvas);
            scene = new GameScene(renderer);
            canvas.Draw += (s, e) => scene.Tick();
        }
    }
}
