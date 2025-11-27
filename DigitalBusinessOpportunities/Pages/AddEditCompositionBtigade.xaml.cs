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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalBusinessOpportunities.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditCompositionBtigade.xaml
    /// </summary>
    public partial class AddEditCompositionBtigade : Page
    {
        public CompositionBrigades material;
        public AddEditCompositionBtigade(CompositionBrigades compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBBrigade.ItemsSource = App.db.Brigades.ToList();
            CBWorker.ItemsSource = App.db.Workers.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if ( CBWorker.SelectedItem == null || CBBrigade.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    
                    var selectedMaterial = CBBrigade.SelectedItem as Brigades;
                    var selectedMaterial1 = CBWorker.SelectedItem as Workers;
                    var newComposition = new CompositionBrigades()
                    {
                        BrigadeId = selectedMaterial.Id,
                        WorkerId = selectedMaterial1.Id
                    };

                    App.db.CompositionBrigades.Add(newComposition);
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
