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
using Circus.DB;

namespace Circus.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPageForAdmin.xaml
    /// </summary>
    public partial class MainMenuPageForAdmin : Page
    {
        public static List<Worker> workers { get; set; }
        public static Worker loggedWorker;

        public MainMenuPageForAdmin()
        {
            InitializeComponent();
            loggedWorker = DBConnection.loginedWorker;
            UserTB.Text = DBConnection.loginedWorker.Surname.ToString() + " " + DBConnection.loginedWorker.Name.ToString() + " " + DBConnection.loginedWorker.Patronymic.ToString();
            LoginTB.Text = DBConnection.loginedWorker.Login;
        }

        private void WorkersBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllWorkersPage());
        }

        private void AnimalsBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllAnimalsPage());
        }

        private void ArtistsBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminToArtistPage());
        }

        private void ServiceStaffBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminToServiceStaffPage());
        }

        private void ReportsBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllReportsPage());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
