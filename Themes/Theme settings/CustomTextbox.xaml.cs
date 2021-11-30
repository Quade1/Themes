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
    public partial class CustomTextbox
    {
        public CustomTextbox()
        {
            InitializeComponent();
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                MessageBox.Show("enter!");
            }
            if (!CheckChar(e.Key.ToString()[0]))
            {
                e.Handled = true;
            }
        }

        private bool CheckChar(char c)
        {
            if (!char.IsWhiteSpace(c) && !char.IsLetterOrDigit(c) && !c.Equals('\n') && !c.Equals('\b'))
            {
                return false;
            }
            return true;
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            DataObject.AddPastingHandler((TextBox)sender, OnPaste);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            foreach (char c in textBox.Text)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    int pos = textBox.SelectionStart;
                    int length = textBox.SelectionLength;
                    textBox.Text = textBox.Text.Replace(c.ToString(), string.Empty);
                    textBox.Select(pos, length);
                }
            }
        }
    }


}
