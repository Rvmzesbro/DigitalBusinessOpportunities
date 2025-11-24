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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
       
        public Main()
        {
            InitializeComponent();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
            
        }

        private void Refresh()
        {
            DGPurchase.ItemsSource = App.db.CompositionOrderMaterials.Where(p => p.OrderId == null).ToList();
        }

        private void BTBuy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddEditMaterials());
        }

        private void BTRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
