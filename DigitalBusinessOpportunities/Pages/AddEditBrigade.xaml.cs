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
    /// Логика взаимодействия для AddEditBrigade.xaml
    /// </summary>
    public partial class AddEditBrigade : Page
    {
        public Brigades material;
        public AddEditBrigade(Brigades compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBEquipment.ItemsSource = App.db.Equipments.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) || CBEquipment.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    
                    var selectedMaterial = CBEquipment.SelectedItem as Equipments;
                    var newComposition = new Brigades()
                    {
                        Name = TBName.Text,
                        EquipmentId = selectedMaterial.Id
                    };

                    App.db.Brigades.Add(newComposition);
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
