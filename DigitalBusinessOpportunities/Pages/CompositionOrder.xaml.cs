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
    /// Логика взаимодействия для CompositionOrder.xaml
    /// </summary>
    public partial class CompositionOrder : Page
    {
        public OrderPurchaseMaterials order;
        public CompositionOrder(OrderPurchaseMaterials orderPurchaseMaterials)
        {
            InitializeComponent();
            order = orderPurchaseMaterials;
            DataContext = order;
            DGComposition.ItemsSource = App.db.CompositionOrderMaterials.Where(p => p.OrderId == order.Id).ToList();
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
