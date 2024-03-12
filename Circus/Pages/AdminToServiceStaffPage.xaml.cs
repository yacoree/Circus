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
    /// Логика взаимодействия для AdminToServiceStaffPage.xaml
    /// </summary>
    public partial class AdminToServiceStaffPage : Page
    {
        public static List<Worker> staffs { get; set; }
        public static List<Taskk> tasks { get; set; }
        public static Worker loggedWorker;
        public AdminToServiceStaffPage()
        {
            InitializeComponent();
            loggedWorker = DBConnection.loginedWorker;
            staffs = DBConnection.circusDB.Worker.ToList();
            tasks = DBConnection.circusDB.Taskk.ToList();
            this.DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            TasksLV.ItemsSource = DBConnection.circusDB.Taskk.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditTaskBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TasksLV.SelectedItem is Taskk task)
            {
                DBConnection.selectedForEditTask = TasksLV.SelectedItem as Taskk;
                EditTaskWindow editTaskWindow = new EditTaskWindow(task);
                editTaskWindow.ShowDialog();
            }
            else if (TasksLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите задачу!");
            }
            Refresh();
        }

        private void AddTaskBTN_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.ShowDialog();
        }

        private void DeleteTaskBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TasksLV.SelectedItem is Taskk task)
            {
                DBConnection.circusDB.Taskk.Remove(task);
                DBConnection.circusDB.SaveChanges();
            }
            else if (TasksLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите задачу!");
            }
            Refresh();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPageForAdmin());
        }
    }
}
