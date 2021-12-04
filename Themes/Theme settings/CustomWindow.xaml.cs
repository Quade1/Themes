using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using System.Windows.Threading;

namespace Themes.Theme_settings
{
    partial class CustomWindow : ResourceDictionary
    {
        public CustomWindow()
        {
            InitializeComponent();
        }

        //private Window window;
        //private Grid move;
        //private Border border;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = (Window)sender;

            WindowChrome windowChrome = new WindowChrome();

            windowChrome.CornerRadius = new CornerRadius(0);
            windowChrome.CaptionHeight = 0;
            windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.None;
            windowChrome.ResizeBorderThickness = new Thickness(5);
            windowChrome.GlassFrameThickness = new Thickness(0);

            WindowChrome.SetWindowChrome(window, windowChrome);

            window.Activated += Window_Activated;
            window.StateChanged += Window_StateChanged;

            InitializeCursorMonitoring(window);
        }

        private void Move_Loaded(object sender, RoutedEventArgs e)
        {
            //move = (Grid)sender;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            //border = (Border)sender;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            window.WindowStyle = WindowStyle.None;
            UpdateBorder(window);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            window.Close();
        }

        private void MaximiseButton_Click(object sender, RoutedEventArgs e)
        {
            // If the winow state is normal then maximise
            // it should be impossible for the else statent to fire
            // but if it does then it will fix some stuff that could be broken

            Window window = Window.GetWindow((DependencyObject)sender);
            Button button = (Button)sender;

            if (window.WindowState == WindowState.Normal)
            {
                Maximise(window);
            }
            else
            {
                Border border = (Border)window.FindChild("MaximisedBorder", typeof(Border));
                border.BorderThickness = new Thickness(0);
                window.WindowState = WindowState.Normal;
            }

        }

        private void MinimiseButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            // Minimise the window and set the border to single border
            // window to play the animation

            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Minimized;
        }

        private void IconImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Move_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            DeactivateChrome(window);
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            Border border = (Border)window.FindChild("MaximisedBorder", typeof(Border));
            Grid move = (Grid)sender;

            border.BorderThickness = new Thickness(1);
            // If the window is being dragged then try restoring it then try drag move
            if (e.ChangedButton == MouseButton.Left)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    double point = window.PointToScreen(e.GetPosition(window)).X;
                    double oldsize = move.ActualWidth;
                    window.WindowState = WindowState.Normal;
                    window.Top = 0;
                    window.Left = point - (point / oldsize * move.ActualWidth);
                }
                window.DragMove();
            }
        }

        private void Maximise(Window window)
        {
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Maximized;
            window.WindowStyle = WindowStyle.None;
        }

        private void UpdateBorder(Window window)
        {
            Border border = (Border)window.FindChild("MaximisedBorder", typeof(Border));
            Button button = (Button)window.FindChild("MaximiseButton", typeof(Button));
            if (window.WindowState == WindowState.Maximized)
            {
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(8);

                button.FontSize = 16;
                button.Content = "🗗︎";
            }
            else
            {
                border.BorderBrush = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(0);

                button.FontSize = 12;
                button.Content = "🗖︎";
            }
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            DisableCheckChrome(sender, new EventArgs());
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            if (window == null) return;
            if (window.IsLoaded)
            {
                FullCheckChrome(window);
            }
        }

        delegate void MyDelegateType(DisableEventArgs e);

        private void InitializeCursorMonitoring(Window window)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (s, e) => {
                DisableCheckChrome(window, new EventArgs());  
            };
            dispatcherTimer.Interval = new TimeSpan(1000);
            dispatcherTimer.Start();

            
        }
        public class DisableEventArgs : EventArgs
        {
            public Window window { get; set; }
        }

        private void FullCheckChrome(Window window)
        {
            if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                ActivateChrome(window);
            }
            else
            {
                DeactivateChrome(window);
            }
        }
        private void DisableCheckChrome(object sender, EventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);
            if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) != System.Windows.Forms.MouseButtons.Left)
            {
                DeactivateChrome(window);
            }
        }

        private void ActivateChrome(Window window)
        {
            WindowChrome windowChrome = WindowChrome.GetWindowChrome(window);
            Border border = (Border)window.FindChild("MaximisedBorder", typeof(Border));
            if (windowChrome != null)
            {
                windowChrome.GlassFrameThickness = new Thickness(1);
                windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.Left | System.Windows.Shell.NonClientFrameEdges.Bottom | System.Windows.Shell.NonClientFrameEdges.Right;
            }
            border.BorderThickness = new Thickness(0, 1, 0, 0);
        }

        private void DeactivateChrome(Window window)
        {
            WindowChrome windowChrome = WindowChrome.GetWindowChrome(window);
            Border border = (Border)window.FindChild("MaximisedBorder", typeof(Border));
            if (windowChrome != null)
            {
                windowChrome.GlassFrameThickness = new Thickness(0);
                windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.None;
            }
            if (window.WindowState == WindowState.Normal)
            {
                border.BorderThickness = new Thickness(0);
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);

            UpdateBorder(window);
        }

        private void Title_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);

            Label label = (Label)sender;
            label.Content = window.Title;
        }

        private void IconImage_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((DependencyObject)sender);

            Image image = (Image)sender;
            image.Source = window.Icon;
        }
    }
}
