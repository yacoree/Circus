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
    /// Логика взаимодействия для AddAnimalWindow.xaml
    /// </summary>
    public partial class AddAnimalWindow : Window
    {
        public static List<Animal> animals { get; set; }
        public static List<Worker> trainers { get; set; }
        public static List<AnimalType> animalTypes { get; set; }
        public static List<Gender> genders { get; set; }
        public static Animal animal = new Animal();

        public AddAnimalWindow()
        {
            InitializeComponent();
            animals = DBConnection.circusDB.Animal.ToList();
            trainers = DBConnection.circusDB.Worker.Where(i => i.ID_Position == 3).ToList();

            animalTypes = DBConnection.circusDB.AnimalType.ToList();
            genders = DBConnection.circusDB.Gender.ToList();
            this.DataContext = this;
        }

        private void AddAnimalBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(NameTB.Text) || string.IsNullOrWhiteSpace(AgeTB.Text) ||
                    string.IsNullOrWhiteSpace(WeightTB.Text) || string.IsNullOrWhiteSpace(TrainerCB.Text) ||
                        string.IsNullOrWhiteSpace(TypeCB.Text))
                {
                    error.AppendLine("Заполните все поля!");
                }
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    animal.Name = NameTB.Text.Trim();
                    animal.Age = int.Parse(AgeTB.Text);
                    animal.Weight = int.Parse(WeightTB.Text);

                    var a = TypeCB.SelectedItem as AnimalType;
                    animal.ID_Type = a.ID;

                    var b = TrainerCB.SelectedItem as Worker;
                    animal.ID_Trainer = a.ID;

                    var c = GenderCB.SelectedItem as Gender;
                    animal.ID_Gender = a.ID;

                    DBConnection.circusDB.Animal.Add(animal);
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
