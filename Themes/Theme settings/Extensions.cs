using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Themes.Theme_settings
{
    public class WindowExtensions : DependencyObject
    {
        public static readonly DependencyProperty ColumnNumber = DependencyProperty.RegisterAttached("ColumnNumber", typeof(int), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: 1, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static int GetColumnNumber(UIElement target)
        {
            int columncount = 1;
            if ((bool)target.GetValue(ShowIcon)) columncount++;

            if ((bool)target.GetValue(ShowMinimise)) columncount++;
            if ((bool)target.GetValue(ShowMaximise)) columncount++;
            if ((bool)target.GetValue(ShowClose)) columncount++;

            return columncount;
        }

        public static readonly DependencyProperty ShowMinimise = DependencyProperty.RegisterAttached("ShowMinimise", typeof(bool), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static bool GetShowMinimise(UIElement target) =>
        (bool)target.GetValue(ShowMinimise);
        public static void SetShowMinimise(UIElement target, bool value) =>
            target.SetValue(ShowMinimise, value);


        public static readonly DependencyProperty ShowMaximise = DependencyProperty.RegisterAttached("ShowMaximise", typeof(bool), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static bool GetShowMaximise(UIElement target) =>
        (bool)target.GetValue(ShowMaximise);
        public static void SetShowMaximise(UIElement target, bool value) =>
            target.SetValue(ShowMaximise, value);


        public static readonly DependencyProperty ShowClose = DependencyProperty.RegisterAttached("ShowClose", typeof(bool), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static bool GetShowClose(UIElement target) =>
        (bool)target.GetValue(ShowClose);
        public static void SetShowClose(UIElement target, bool value) =>
            target.SetValue(ShowClose, value);


        public static readonly DependencyProperty ShowTitle = DependencyProperty.RegisterAttached("ShowTitle", typeof(bool), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static bool GetShowTitle(UIElement target) =>
        (bool)target.GetValue(ShowTitle);
        public static void SetShowTitle(UIElement target, bool value) =>
            target.SetValue(ShowTitle, value);


        public static readonly DependencyProperty ShowIcon = DependencyProperty.RegisterAttached("ShowIcon", typeof(bool), typeof(WindowExtensions), new FrameworkPropertyMetadata(defaultValue: true, flags: FrameworkPropertyMetadataOptions.AffectsRender));
        public static bool GetShowIcon(UIElement target) =>
        (bool)target.GetValue(ShowIcon);
        public static void SetShowIcon(UIElement target, bool value) =>
            target.SetValue(ShowIcon, value);

    }

    public static class UIChildFinder
    {
        public static DependencyObject FindChild(this DependencyObject reference, string childName, Type childType)
        {
            DependencyObject foundChild = null;
            if (reference != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    // If the child is not of the request child type child
                    if (child.GetType() != childType)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild(child, childName, childType);
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = child;
                        break;
                    }
                }
            }
            return foundChild;
        }
    }
}
