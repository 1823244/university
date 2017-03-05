#region using

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
using Windows.UI.Popups;
using System.Diagnostics;

#endregion

namespace Pentomino
{
    public sealed partial class MainPage : Page
    {
        
        private int[,] currentLevel;

        public MainPage()
        {   
            this.InitializeComponent();
      
            Grid[] grids = {F, I, L, N, P, T, U, V, W, X, Y, Z };
            Data.InitializeFigures(grids);
            
            currentLevel = new int[6, 10];
            Data.InitializeLevel(Field, currentLevel, Tester);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Object_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Data.Motion(e);
        }

        private void RightTap_Rotate(object sender, RightTappedRoutedEventArgs e)
        {
            Data.Rotation(sender, e);  
        }

        private void Object_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            Data.MotionOver();
        }

        private void Object_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            Data.MotionStarted(sender, e);
        }

        private void ResetPage(object sender, RoutedEventArgs e)
        {
            var _Frame = Window.Current.Content as Frame;
            _Frame.Navigate(_Frame.Content.GetType());
            _Frame.GoBack();
        }

        private void FindOneSolution(object sender, RoutedEventArgs e)
        {
            Data.FindSolution();
            this.Frame.Navigate(typeof(BlankPage1));
        }
    }
}