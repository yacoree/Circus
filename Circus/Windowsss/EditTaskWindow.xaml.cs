using Circus.DB;
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
using System.Windows.Shapes;

namespace Circus.Windowsss
{
    /// <summary>
    /// Логика взаимодействия для EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public static List<Worker> staffs { get; set; }
        public static List<Taskk> tasks { get; set; }
        Taskk contextTask;
        public EditTaskWindow(Taskk task)
        {
            InitializeComponent();
            contextTask = task;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void InitializeDataInPage()
        {
            tasks = DBConnection.circusDB.Taskk.ToList();
            staffs = DBConnection.circusDB.Worker.ToList();
            this.DataContext = this;
            DateTimeDP.Text = contextTask.DateTime.ToString();
            StaffCB.SelectedIndex = (int)contextTask.ID_ServiceStaff - 1;
            DescriptionTB.Text = contextTask.Description;
            MyCheckBox.IsChecked = false;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                Taskk task = contextTask;
                if (string.IsNullOrWhiteSpace(StaffCB.Text) ||
                    string.IsNullOrWhiteSpace(DescriptionTB.Text))
                {
                    error.AppendLine("Заполните все поля!");
                }
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    task.Description = DescriptionTB.Text;
                    task.ID_ServiceStaff = (StaffCB.SelectedItem as Worker).ID;
                    task.DateTime = DateTime.Now;
                    task.DateTime = DateTimeDP.SelectedDate;
                    DBConnection.circusDB.SaveChanges();

                    DescriptionTB.Text = String.Empty;
                    StaffCB.Text = String.Empty;

                    DBConnection.circusDB.SaveChanges();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
