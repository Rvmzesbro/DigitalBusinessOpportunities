using DigitalBusinessOpportunities.Models;
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

namespace DigitalBusinessOpportunities.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditEquipment.xaml
    /// </summary>
    public partial class AddEditEquipment : Page
    {
        public Equipments material;
        public AddEditEquipment(Equipments compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBWorkshop.ItemsSource = App.db.Workshops.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) || CBWorkshop.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    
                    var selectedMaterial = CBWorkshop.SelectedItem as Workshops;
                    var newComposition = new Equipments()
                    {
                        Name = TBName.Text,
                        WorkshopId = selectedMaterial.Id
                    };

                    App.db.Equipments.Add(newComposition);
                }


                App.db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
