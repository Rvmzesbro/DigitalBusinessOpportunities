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
    /// Логика взаимодействия для AddEditProduction.xaml
    /// </summary>
    public partial class AddEditProduction : Page
    {
        public CompositionProductions product;
        public AddEditProduction(CompositionProductions composition)
        {
            InitializeComponent();
            Bindings();
            product = composition;
            DataContext = product;
        }

        private void Bindings()
        {
            CBNomenclature.ItemsSource = App.db.Nomenclatures.Where(p => p.Type == "готовая продукция" || p.Type == "полуфабрикат").ToList();
            CBSpecifications.ItemsSource = App.db.Specifications.Where(p => (p.Nomenclatures.Type == "готовая продукция" || p.Nomenclatures.Type == "полуфабрикат") && p.Status == "активная").ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBNomenclature.SelectedItem == null || CBSpecifications.SelectedItem == null || string.IsNullOrWhiteSpace(TBUnit.Text))
            {
                return;
            }
            else
            {
                if (product == null)
                {
                    if (!decimal.TryParse(TBCount.Text, out decimal result1)) return;
                    var specification = CBSpecifications.SelectedItem as Specifications;
                    var nomenclature = CBNomenclature.SelectedItem as Nomenclatures;
                    var NewProduct = new CompositionProductions()
                    {
                        OrderId = null,
                        NomenclatureId = nomenclature.Id,
                        Count = decimal.Parse(TBCount.Text),
                        Unit = TBUnit.Text,
                        SpecificationId = specification.Id
                    };
                    App.db.CompositionProductions.Add(NewProduct);
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
