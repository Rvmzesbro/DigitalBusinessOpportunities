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
    /// Логика взаимодействия для AddEditStageSpecification.xaml
    /// </summary>
    public partial class AddEditStageSpecification : Page
    {
        public StageSpecifications material;
        public AddEditStageSpecification(StageSpecifications compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBSpecification.ItemsSource = App.db.Specifications.ToList();
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBNameStage.Text) || string.IsNullOrWhiteSpace(TBNumberStage.Text) || CBSpecification.SelectedItem == null)
            {
                return;
            }
            else
            {

                if (material == null)
                {
                    if (!int.TryParse(TBNumberStage.Text, out int result)) return;
                   
                    var selectedMaterial = CBSpecification.SelectedItem as Specifications;
                    var newComposition = new StageSpecifications()
                    {
                        SpecificationId = selectedMaterial.Id,
                        NumberStage = result,
                        NameStage = TBNameStage.Text
                    };

                    App.db.StageSpecifications.Add(newComposition);
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
