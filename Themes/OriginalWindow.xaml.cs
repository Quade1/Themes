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

namespace Themes
{
    public partial class OriginalWindow
    {
        public OriginalWindow()
        {
            InitializeComponent();
        }

        Window window;
        Grid move;
        Border border;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            window = (Window)sender;

            WindowChrome windowChrome = new WindowChrome();

            windowChrome.CornerRadius = new CornerRadius(0);
            windowChrome.CaptionHeight = 0;
            windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.None;
            windowChrome.ResizeBorderThickness = new Thickness(5);
            windowChrome.GlassFrameThickness = new Thickness(0);

            WindowChrome.SetWindowChrome(window, windowChrome);

            window.Activated += Window_Activated;
            window.StateChanged += Window_StateChanged;

            InitializeCursorMonitoring();
        }

        private void Move_Loaded(object sender, RoutedEventArgs e)
        {
            move = (Grid)sender;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            border = (Border)sender;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            window.WindowStyle = WindowStyle.None;
            UpdateBorder();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void MaximiseButton_Click(object sender, RoutedEventArgs e)
        {
            // If the winow state is normal then maximise
            // it should be impossible for the else statent to fire
            // but if it does then it will fix some stuff that could be broken

            if (window.WindowState == WindowState.Normal)
            {
                Maximise();
            }
            else
            {
                window.WindowState = WindowState.Normal;
                window.BorderThickness = new Thickness(0);
            }

        }

        private void MinimiseButton_Click(object sender, RoutedEventArgs e)
        {
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
            DeactivateChrome();
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
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

        private void Maximise()
        {
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Maximized;
            window.WindowStyle = WindowStyle.None;
        }

        private void UpdateBorder()
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.BorderBrush = new SolidColorBrush(Colors.Black);
                window.BorderThickness = new Thickness(8);
            }
            else
            {
                window.BorderThickness = new Thickness(0);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            DisableCheckChrome(sender, new EventArgs());
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (window == null) return;
            if (window.IsLoaded)
            {
                FullCheckChrome();
            }
        }

        private void InitializeCursorMonitoring()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DisableCheckChrome;
            dispatcherTimer.Interval = new TimeSpan(1000);
            dispatcherTimer.Start();
        }

        private void FullCheckChrome()
        {
            if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                ActivateChrome();
            }
            else
            {
                DeactivateChrome();
            }
        }
        private void DisableCheckChrome(object sender, EventArgs e)
        {
            if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) != System.Windows.Forms.MouseButtons.Left)
            {
                DeactivateChrome();
            }
        }

        private void ActivateChrome()
        {
            WindowChrome windowChrome = WindowChrome.GetWindowChrome(window); 
            if (windowChrome != null)
            {
                windowChrome.GlassFrameThickness = new Thickness(1);
                windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.Left | System.Windows.Shell.NonClientFrameEdges.Bottom | System.Windows.Shell.NonClientFrameEdges.Right;
            }
            border.BorderThickness = new Thickness(0, 1, 0, 0);
        }

        private void DeactivateChrome()
        {
            WindowChrome windowChrome = WindowChrome.GetWindowChrome(window);
            if (windowChrome != null)
            {
                windowChrome.GlassFrameThickness = new Thickness(0);
                windowChrome.NonClientFrameEdges = System.Windows.Shell.NonClientFrameEdges.None;
            }
            border.BorderThickness = new Thickness(0);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            UpdateBorder();
        }

        private void Title_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label) sender;
            label.Content = window.Title;
        }

        private void IconImage_Loaded(object sender, RoutedEventArgs e)
        {
            Image image = (Image)sender;
            image.Source = window.Icon;
        }
    }
}
