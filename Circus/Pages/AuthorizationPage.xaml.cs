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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Worker> workers { get; set; }

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EnterBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginTB.Text.Trim();
                string password = PasswordTB.Password.Trim();

                workers = new List<Worker>(DBConnection.circusDB.Worker.ToList());
                var currentWorker = workers.FirstOrDefault(i => i.Login.Trim() == login && i.Password.Trim() == password);
                DBConnection.loginedWorker = currentWorker;
                if (currentWorker != null && currentWorker.ID_Position == 1)
                {
                    NavigationService.Navigate(new MainMenuPageForAdmin());
                }
                else if (currentWorker != null && currentWorker.ID_Position == 2)
                {
                    NavigationService.Navigate(new MainMenuPageForArtist());
                }
                else if (currentWorker != null && currentWorker.ID_Position == 3)
                {
                    NavigationService.Navigate(new MainMenuPageForTrainer());
                }
                //else if (currentWorker != null && currentWorker.ID_Position == 4)
                //{
                //    NavigationService.Navigate(new MainMenuPageForServiceStaff());
                //}
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }
        }
    }
}
