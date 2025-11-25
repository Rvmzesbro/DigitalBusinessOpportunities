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
    /// Логика взаимодействия для AddEditSales.xaml
    /// </summary>
    public partial class AddEditSales : Page
    {
        public CompositionSales sales;
        public AddEditSales(CompositionSales compositionSales)
        {
            InitializeComponent();
            Bindings();
            sales = compositionSales;
            DataContext = sales;
        }

        private void Bindings()
        {
            CBMaterial.ItemsSource = App.db.Nomenclatures.Where(p => p.Type == "готовая продукция").ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPrice.Text) || string.IsNullOrWhiteSpace(TBCount.Text) || CBMaterial.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (sales == null)
                {
                    if (!decimal.TryParse(TBPrice.Text, out decimal result)) return;
                    if (!decimal.TryParse(TBCount.Text, out decimal result1)) return;
                    var selectedMaterial = CBMaterial.SelectedItem as Nomenclatures;

                    var newComposition = new CompositionSales()
                    {
                        NomenclatureId = selectedMaterial.Id,
                        Price = decimal.Parse(TBPrice.Text),
                        Count = decimal.Parse(TBCount.Text),
                        SaleId = null,
                        WarehouseId = 2
                    };

                    App.db.CompositionSales.Add(newComposition);
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
