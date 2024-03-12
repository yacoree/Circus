using Circus.DB;
using Circus.Windowsss;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Circus.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminToArtistPage.xaml
    /// </summary>
    public partial class AdminToArtistPage : Page
    {
        public static List<Worker> artists { get; set; }
        public static List<Timetable> timetables { get; set; }
        public static Worker loggedWorker;
        public AdminToArtistPage()
        {
            InitializeComponent();
            loggedWorker = DBConnection.loginedWorker;
            artists = DBConnection.circusDB.Worker.ToList();
            timetables = DBConnection.circusDB.Timetable.ToList();
            this.DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            TimetablesLV.ItemsSource = DBConnection.circusDB.Timetable.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditTimetableBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TimetablesLV.SelectedItem is Timetable timetable)
            {
                DBConnection.selectedForEditTimetable = TimetablesLV.SelectedItem as Timetable;
                EditTimetableWindow editTimetableWindow = new EditTimetableWindow(timetable);
                editTimetableWindow.ShowDialog();
            }
            else if (TimetablesLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите расписание!");
            }
            Refresh();
        }

        private void AddTimetableBTN_Click(object sender, RoutedEventArgs e)
        {
            AddTimetableWindow addTimetableWindow = new AddTimetableWindow();
            addTimetableWindow.ShowDialog();
        }

        private void DeleteTimetableBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TimetablesLV.SelectedItem is Timetable timetable)
            {
                DBConnection.circusDB.Timetable.Remove(timetable);
                DBConnection.circusDB.SaveChanges();
            }
            else if (TimetablesLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите расписание!");
            }
            Refresh();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPageForAdmin());
        }
    }
}
