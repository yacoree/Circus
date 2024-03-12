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
using Circus.Pages;
using Circus.DB;
using Microsoft.Win32;
using System.IO;

namespace Circus.Windowsss
{
    /// <summary>
    /// Логика взаимодействия для EditWorkerWindow.xaml
    /// </summary>
    public partial class EditWorkerWindow : Window
    {
        public static List<Worker> workers { get; set; }
        public static List<Position> positions { get; set; }
        Worker contextWorker;
        public EditWorkerWindow(Worker worker)
        {
            InitializeComponent();
            contextWorker = worker;
            InitializeDataInPage();
            this.DataContext = this;

        }
        private void InitializeDataInPage()
        {
            workers = DBConnection.circusDB.Worker.ToList();
            positions = DBConnection.circusDB.Position.ToList();
            this.DataContext = this;
            SurnameTB.Text = contextWorker.Surname;
            NameTB.Text = contextWorker.Name;
            PatronymicTB.Text = contextWorker.Patronymic;
            DateOfBirthDP.SelectedDate = contextWorker.DateOfBirth;
            PhoneTB.Text = contextWorker.Phone;
            PositionCB.SelectedIndex = (int)contextWorker.ID_Position - 1;
            LoginTB.Text = contextWorker.Login;
            PasswordTB.Text = contextWorker.Password;
            if (contextWorker.Photo != null)
            {
                PhotoWorker.Source = new BitmapImage(new Uri(contextWorker.Photo.ToString()));
            }
        }
        private void AddPhotoBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                DBConnection.loginedWorker.Photo = File.ReadAllBytes(openFileDialog.FileName);
                PhotoWorker.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                Worker worker = contextWorker;
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
                    worker.Surname = SurnameTB.Text;
                    worker.Name = NameTB.Text;
                    worker.Patronymic = PatronymicTB.Text;
                    //worker.DateOfBirth = DateOfBirthDP.SelectedDate;
                    worker.ID_Position = (PositionCB.SelectedItem as Position).ID;
                    worker.Phone = PhoneTB.Text;
                    worker.Login = LoginTB.Text;
                    worker.Password = PasswordTB.Text;
                    DBConnection.circusDB.SaveChanges();

                    SurnameTB.Text = String.Empty;
                    NameTB.Text = String.Empty;
                    PatronymicTB.Text = String.Empty;
                    DateOfBirthDP = null;
                    PositionCB.Text = String.Empty;
                    PositionCB.Text = String.Empty;
                    PhoneTB.Text = String.Empty;
                    LoginTB.Text = String.Empty;
                    PasswordTB.Text = String.Empty;

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
