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
    /// Логика взаимодействия для AddEditMaterialProduction.xaml
    /// </summary>
    public partial class AddEditMaterialProduction : Page
    {
        public MaterialProductions material;
        public AddEditMaterialProduction(MaterialProductions materialProductions)
        {
            InitializeComponent();
            Bindings();
            material = materialProductions;
            DataContext = material;
        }

        private void Bindings()
        {
            CBNomenclature.ItemsSource = App.db.Nomenclatures.Where(p => p.Type != "готовая продукция").ToList();
            CBStage.ItemsSource = App.db.StageProductions.Select(p => p.Id).ToList();

        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CBStage.SelectedItem == null || CBNomenclature.SelectedItem == null || string.IsNullOrWhiteSpace(TBUnit.Text) || string.IsNullOrWhiteSpace(TBCount.Text))
            {
                return;
            }
            else
            {
                if (material == null)
                {
                   
                   
                    if (!decimal.TryParse(TBCount.Text, out decimal result)) return;
                    var nomenclature = CBNomenclature.SelectedItem as Nomenclatures;
                    var stage = CBStage.SelectedItem as StageProductions;

                    var NewProduct = new MaterialProductions()
                    {
                        StageId = CBStage.SelectedIndex + 1,
                        NomenclatureId = nomenclature.Id,
                        Count = decimal.Parse(TBCount.Text),
                        Unit = TBUnit.Text,
                    };
                    App.db.MaterialProductions.Add(NewProduct);
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
