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
        TextBox tb;

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
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
            {
                e.Handled = true;
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
    }


}
