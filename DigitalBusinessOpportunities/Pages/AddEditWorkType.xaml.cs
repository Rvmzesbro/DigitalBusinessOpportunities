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
    /// Логика взаимодействия для AddEditWorkType.xaml
    /// </summary>
    public partial class AddEditWorkType : Page
    {
        public WorkTypes material;
        public AddEditWorkType(WorkTypes compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) || string.IsNullOrWhiteSpace(TBCostPerHour.Text) )
            {
                return;
            }
            else
            {

                if (material == null)
                {

                    if (!decimal.TryParse(TBCostPerHour.Text, out decimal result1)) return;
                    var newComposition = new WorkTypes()
                    {
                        Name = TBName.Text,
                        CostPerHour = result1
                    };

                    App.db.WorkTypes.Add(newComposition);
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
