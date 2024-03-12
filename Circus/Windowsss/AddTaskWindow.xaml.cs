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
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public static List<Worker> workers { get; set; }
        public static List<Taskk> tasks { get; set; }
        public static Taskk task = new Taskk();

        public AddTaskWindow()
        {
            InitializeComponent();
            workers = DBConnection.circusDB.Worker.Where(i => i.ID_Position == 4).ToList();
            tasks = DBConnection.circusDB.Taskk.ToList();
            this.DataContext = this;
        }

        private void AddTaskBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(StaffCB.Text) || string.IsNullOrWhiteSpace(DescriptionTB.Text))
                {
                    error.AppendLine("Заполните все поля!");
                }
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    task.Description = DescriptionTB.Text.Trim();
                    task.DateTime = DateTime.Now;
                    task.DateTime = DateTimeDP.SelectedDate;
                    MyCheckBox.IsChecked = false;
                    task.DoneOrNo = MyCheckBox.IsChecked.Value;
                    var a = StaffCB.SelectedItem as Worker;
                    task.ID_ServiceStaff = a.ID;

                    DBConnection.circusDB.Taskk.Add(task);
                    DBConnection.circusDB.SaveChanges();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Заполните все поля!");
            }
            Close();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
