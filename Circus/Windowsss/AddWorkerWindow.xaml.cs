using Circus.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        public static List<Worker> workers { get; set; }
        public static List<Position> positions { get; set; }
        public static Worker worker = new Worker();

        public AddWorkerWindow()
        {
            InitializeComponent();
            workers = DBConnection.circusDB.Worker.ToList();
            positions = DBConnection.circusDB.Position.ToList();
            this.DataContext = this;
        }
        private void AddWorkerBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(SurnameTB.Text) || string.IsNullOrWhiteSpace(NameTB.Text) ||
                    string.IsNullOrWhiteSpace(PatronymicTB.Text) ||
                        DateOfBirthDP.SelectedDate == null || string.IsNullOrWhiteSpace(PhoneTB.Text) ||
                        string.IsNullOrWhiteSpace(LoginTB.Text) || string.IsNullOrWhiteSpace(PasswordTB.Text))
                {
                    error.AppendLine("Заполните все поля!");
                }
                if (DateOfBirthDP.SelectedDate != null && (DateTime.Now - (DateTime)DateOfBirthDP.SelectedDate).TotalDays < 365 * 18 + 4)
                {
                    error.AppendLine("Сотрудник не может быть младше 18 лет.");
                }
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    worker.Surname = SurnameTB.Text.Trim();
                    worker.Name = NameTB.Text.Trim();
                    worker.Patronymic = PatronymicTB.Text.Trim();
                    worker.Phone = PhoneTB.Text.Trim();
                    worker.DateOfBirth = DateOfBirthDP.SelectedDate;
                    worker.Login = LoginTB.Text.Trim();
                    worker.Password = PasswordTB.Text.Trim();
                    var a = PositionCB.SelectedItem as Position;
                    worker.ID_Position = a.ID;

                    DBConnection.circusDB.Worker.Add(worker);
                    DBConnection.circusDB.SaveChanges();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
