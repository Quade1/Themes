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

namespace Themes.Custom_Controls
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : Window
    {
        string output;

        public Input()
        {
            InitializeComponent();
        }

        public void GetInput(string Title, string Question)
        {
            //this.Title = Title;
            //this.Question.Content = Question;
            ShowDialog();

            //return output;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Click to add text")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                textBox.Text = "Click to add text";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            output = InputBox.Text;
            Close();
        }
    }
}
