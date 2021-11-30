using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Themes.Custom_Controls;

namespace Themes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input input = new Input();
            input.GetInput("Name", "What's your name?");

            Focus();


            /*Input input = new Input();
            input.ShowDialog();*/
            /*string name =*/ //input.GetInput("Name", "What's your name?");
            //this.Input.Content = name;
        }
    }
}
