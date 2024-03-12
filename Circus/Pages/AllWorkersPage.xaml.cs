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
using Circus.Windowsss;
using MaterialDesignThemes.Wpf;
using Circus.DB;

namespace Circus.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllWorkersPage.xaml
    /// </summary>
    public partial class AllWorkersPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static Worker loggedWorker;

        public AllWorkersPage()
        {
            InitializeComponent();
            loggedWorker = DBConnection.loginedWorker;
            workers = DBConnection.circusDB.Worker.ToList();
            this.DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            WorkersLV.ItemsSource = DBConnection.circusDB.Worker.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WorkersLV.SelectedItem is Worker worker)
            {
                DBConnection.selectedForEditWorker = WorkersLV.SelectedItem as Worker;
                EditWorkerWindow editWorkerWindow = new EditWorkerWindow(worker);
                editWorkerWindow.ShowDialog();
            }
            else if (WorkersLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите сотрудника!");
            }
            Refresh();
        }

        private void AddWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow addWorkerWindow = new AddWorkerWindow();
            addWorkerWindow.ShowDialog();
        }

        private void DeleteWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WorkersLV.SelectedItem is Worker work)
            {
                DBConnection.circusDB.Worker.Remove(work);
                DBConnection.circusDB.SaveChanges();
            }
            else if (WorkersLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите сотрудника!");
            }
            Refresh();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTB.Text.Length > 0)

                WorkersLV.ItemsSource = DBConnection.circusDB.Worker.Where(i => i.Surname.ToLower().StartsWith(SearchTB.Text.Trim().ToLower())
                || i.Name.ToLower().StartsWith(SearchTB.Text.Trim().ToLower()) || i.Patronymic.ToLower().StartsWith(SearchTB.Text.Trim().ToLower())).ToList();

            else
                WorkersLV.ItemsSource = new List<Worker>(DBConnection.circusDB.Worker.ToList());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPageForAdmin());
        }
    }
}
