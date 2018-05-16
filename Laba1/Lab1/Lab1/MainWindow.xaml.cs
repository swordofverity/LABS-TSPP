using Lab1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private EventContext context = new EventContext();
        private List<Venue> venues;

        public List<Venue> Venues { get { return venues; } set { venues = value; RaisePropertyChanged("Venues"); } }
        private List<Event> events;

        public List<Event> Events { get { return events; } set { events = value; RaisePropertyChanged("Events"); } }
        private List<Reservation> reservations;

        public List<Reservation> Reservations { get { return reservations; } set { reservations = value; RaisePropertyChanged("Reservations"); } }
        private List<Customer> customers;

        public List<Customer> Customers { get { return customers; } set { customers = value; RaisePropertyChanged("Customers"); } }

        public MainWindow()
        {
            InitializeComponent();
            venues = context.Venues.ToList();
            customers = context.Customers.ToList();
            DataContext = this;
            
            //dataGrid.ItemsSource = c.Venues.ToList();
            //dataGrid_Copy.ItemsSource = c.Events.ToList();
            //dataGrid_Copy2.ItemsSource = c.Reservations.ToList();
            //dataGrid_Copy3.ItemsSource = c.Customers.ToList();
        }

        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("О программе", "Лабораторная работа №1\nВыполнил:\nстудент 327ст группы Рец Владмир", MessageDialogStyle.Affirmative);
        }


        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int cID = (listBox_Copy2.SelectedItem as Customer).CustomerID;
                Reservations = context.Reservations.Where(x => x.CustomerID == cID).ToList();
            }catch { }
        }

        private void listBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int vID = (listBox.SelectedItem as Venue).VenueID;
                Events = context.Events.Where(x => x.VenueID == vID).ToList();
            }
            catch { }
        }

        private void listBox_Copy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int eID = (listBox_Copy.SelectedItem as Event).EventID;
                Reservations = context.Reservations.Where(x => x.EventID == eID).ToList();
            }
            catch { }
        }
    }
}
