using System;
using System.Data.Entity;
using System.Windows;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            Database.SetInitializer<Model.EventContext>(new Model.Initializer());
        }
    }
}
