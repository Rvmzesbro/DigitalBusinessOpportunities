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
    /// Логика взаимодействия для AddEditMaterialSpecification.xaml
    /// </summary>
    public partial class AddEditMaterialSpecification : Page
    {
        public MaterialSpecifications material;
        public AddEditMaterialSpecification(MaterialSpecifications compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBNomenclature.ItemsSource = App.db.Nomenclatures.Where(p => p.Type == "готовая продукция" || p.Type == "полуфабрикат").ToList();
            CBMaterial.ItemsSource = App.db.Nomenclatures.Where(p => p.Type == "сырье" || p.Type == "полуфабрикат").ToList();
            
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBMaterial.SelectedItem == null || CBStage.SelectedItem == null || string.IsNullOrWhiteSpace(TBUnit.Text))
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    
                    if (!decimal.TryParse(TBCount.Text, out decimal result1)) return;
                    var selectedMaterial = CBStage.SelectedItem as StageSpecifications;
                    var selectedMaterial1 = CBMaterial.SelectedItem as Nomenclatures;
                    var selectedMaterial2 = CBNomenclature.SelectedItem as Nomenclatures;
                    var newComposition = new MaterialSpecifications()
                    {
                        StageId = selectedMaterial.Id,
                        MaterialId = selectedMaterial1.Id,
                        NomenclatureId = selectedMaterial2.Id,
                        Count = result1,
                        Unit = TBUnit.Text
                    };

                    App.db.MaterialSpecifications.Add(newComposition);
                }


                App.db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CBNomenclature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBNomenclature.SelectedItem == null)
            {
                CBStage.ItemsSource = null;
                return;
            }
            else
            {
                var nomenclature = CBNomenclature.SelectedItem as Nomenclatures;
                CBStage.ItemsSource = App.db.StageSpecifications.Where(p => p.Specifications.NomenclatureId == nomenclature.Id).ToList();
            }
        }
    }
}
