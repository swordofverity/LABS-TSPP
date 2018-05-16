using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab1
{
    static class HelpProvider
    {
        static HelpProvider()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof(FrameworkElement),
                new CommandBinding(
                    ApplicationCommands.Help,
                    new ExecutedRoutedEventHandler(Executed),
                    new CanExecuteRoutedEventHandler(CanExecute)));
        }

        static private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            FrameworkElement senderElement = sender as FrameworkElement;

            if (HelpProvider.GetHelpString(senderElement) != null)
                e.CanExecute = true;
        }

        static private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Help: " + HelpProvider.GetHelpString(sender as FrameworkElement));
            System.Diagnostics.Process.Start(@"C:\Users\root\Desktop\учеба\TSPP\Laba3\spravka.html");//Open chm file
        }

        public static string GetHelpString(DependencyObject obj)
        {
            return (string)obj.GetValue(HelpStringProperty);
        }

        public static void SetHelpString(DependencyObject obj, string value)
        {
            obj.SetValue(HelpStringProperty, value);
        }

        public static readonly DependencyProperty HelpStringProperty =
            DependencyProperty.RegisterAttached("HelpString", typeof(string), typeof(HelpProvider));
    }
}
