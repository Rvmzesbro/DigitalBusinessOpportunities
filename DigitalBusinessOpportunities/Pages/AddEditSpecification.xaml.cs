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
    /// Логика взаимодействия для AddEditSpecification.xaml
    /// </summary>
    public partial class AddEditSpecification : Page
    {
        public Specifications material;
        public List<string> Statuses = new List<string>()
        {
            "активная",
            "архивная"
        };
        public AddEditSpecification(Specifications compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBNomenclature.ItemsSource = App.db.Nomenclatures.Where(p => p.Type == "готовая продукция" || p.Type == "полуфабрикат").ToList();
            CBStatus.ItemsSource = Statuses;
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBUnit.Text) || string.IsNullOrWhiteSpace(TBCount.Text) || CBNomenclature.SelectedItem == null || CBStatus.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    
                    if (!decimal.TryParse(TBCount.Text, out decimal result1)) return;
                    var selectedMaterial = CBNomenclature.SelectedItem as Nomenclatures;
                   
                    var newComposition = new Specifications()
                    {
                        NomenclatureId = selectedMaterial.Id,
                        Count = result1,
                        Unit = TBUnit.Text,
                        Status = CBStatus.SelectedItem.ToString()
                    };

                    App.db.Specifications.Add(newComposition);
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
