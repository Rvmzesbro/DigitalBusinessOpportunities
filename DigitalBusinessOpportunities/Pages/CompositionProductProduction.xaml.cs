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
    /// Логика взаимодействия для CompositionProductProduction.xaml
    /// </summary>
    public partial class CompositionProductProduction : Page
    {
        public OrderProductions order;
        public CompositionProductProduction(OrderProductions orderProductions)
        {
            InitializeComponent();
            order = orderProductions;
            DataContext = order;
            DGComposition.ItemsSource = App.db.CompositionProductions.Where(p => p.OrderId == order.Id).ToList();
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
