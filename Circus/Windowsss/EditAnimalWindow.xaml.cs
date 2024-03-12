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
    /// Логика взаимодействия для EditAnimalWindow.xaml
    /// </summary>
    public partial class EditAnimalWindow : Window
    {
        public static List<Animal> animals {  get; set; }
        public static List<Worker> trainers { get; set; }
        public static List<Gender> genders { get; set; }
        public static List<AnimalType> animalTypes { get; set; }
        Animal contextAnimal;
        public EditAnimalWindow(Animal animal)
        {
            InitializeComponent();
            contextAnimal = animal;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void InitializeDataInPage()
        {
            animals = DBConnection.circusDB.Animal.ToList();
            trainers = DBConnection.circusDB.Worker.ToList();
            animalTypes = DBConnection.circusDB.AnimalType.ToList();
            genders = DBConnection.circusDB.Gender.ToList();
            this.DataContext = this;
            NameTB.Text = contextAnimal.Name;
            AgeTB.Text = contextAnimal.Age.ToString();
            WeightTB.Text = contextAnimal.Weight.ToString();
            GenderCB.SelectedIndex = (int)contextAnimal.ID_Gender - 1;
            TrainerCB.SelectedIndex = (int)contextAnimal.ID_Trainer - 1;
            TypeCB.SelectedIndex = (int)contextAnimal.ID_Type - 1;

            FoodPreferenceTB.Text = contextAnimal.FoodPreference;
            CareRecommendationsTB.Text = contextAnimal.CareRecommendations;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                Animal animal = contextAnimal;
                if (string.IsNullOrWhiteSpace(NameTB.Text) ||
                    string.IsNullOrWhiteSpace(AgeTB.Text) || string.IsNullOrWhiteSpace(WeightTB.Text) ||
                        string.IsNullOrWhiteSpace(GenderCB.Text) || string.IsNullOrWhiteSpace(TrainerCB.Text) || string.IsNullOrWhiteSpace(TypeCB.Text))
                {
                    error.AppendLine("Заполните все поля!");
                }
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    animal.Name = NameTB.Text;
                    animal.Age = int.Parse(AgeTB.Text);
                    animal.Weight = int.Parse(WeightTB.Text);
                    animal.ID_Trainer = (TrainerCB.SelectedItem as Worker).ID;
                    animal.ID_Gender = (GenderCB.SelectedItem as Gender).ID;
                    animal.ID_Type = (TypeCB.SelectedItem as AnimalType).ID;
                    animal.FoodPreference = FoodPreferenceTB.Text;
                    animal.CareRecommendations = CareRecommendationsTB.Text;
                    DBConnection.circusDB.SaveChanges();

                    NameTB.Text = String.Empty;
                    AgeTB.Text = String.Empty;
                    WeightTB.Text = String.Empty;
                    TrainerCB.Text = String.Empty;
                    TypeCB.Text = String.Empty;
                    GenderCB.Text = String.Empty;
                    FoodPreferenceTB.Text = String.Empty;
                    CareRecommendationsTB.Text = String.Empty;

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
