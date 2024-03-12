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
    /// Логика взаимодействия для AllAnimalsPage.xaml
    /// </summary>
    public partial class AllAnimalsPage : Page
    {
        public static List<Animal> animals { get; set; }

        public AllAnimalsPage()
        {
            InitializeComponent();
            animals = DBConnection.circusDB.Animal.ToList();
            this.DataContext = this;
            Refresh();
        }

        private void Refresh()
        {
            AnimalsLV.ItemsSource = DBConnection.circusDB.Animal.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditAnimalBTN_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalsLV.SelectedItem is Animal animal)
            {
                DBConnection.selectedForEditAnimal = AnimalsLV.SelectedItem as Animal;
                EditAnimalWindow editAnimalWindow = new EditAnimalWindow(animal);
                editAnimalWindow.ShowDialog();
            }
            else if (AnimalsLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите животное!");
            }
            Refresh();
        }

        private void AddAnimalBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAnimalWindow addAnimalWindow = new AddAnimalWindow();
            addAnimalWindow.ShowDialog();
        }

        private void DeleteAnimalBTN_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalsLV.SelectedItem is Animal anim)
            {
                DBConnection.circusDB.Animal.Remove(anim);
                DBConnection.circusDB.SaveChanges();
            }
            else if (AnimalsLV.SelectedItem is null)
            {
                MessageBox.Show("Выберите животное!");
            }
            Refresh();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTB.Text.Length > 0)

                AnimalsLV.ItemsSource = DBConnection.circusDB.Animal.Where(i => i.Name.ToLower().StartsWith(SearchTB.Text.Trim().ToLower())).ToList();

            else
                AnimalsLV.ItemsSource = new List<Animal>(DBConnection.circusDB.Animal.ToList());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPageForAdmin());
        }
    }
}
