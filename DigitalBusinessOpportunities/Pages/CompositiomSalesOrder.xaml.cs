using DigitalBusinessOpportunities;
using DigitalBusinessOpportunities.Models;
using DigitalBusinessOpportunities.Pages;
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
    /// Логика взаимодействия для CompositiomSalesOrder.xaml
    /// </summary>
    public partial class CompositiomSalesOrder : Page
    {
        public Sales order;
        public CompositiomSalesOrder(Sales sales)
        {
            InitializeComponent();
            order = sales;
            DataContext = order;
            DGComposition.ItemsSource = App.db.CompositionSales.Where(p => p.SaleId == order.Id).ToList();
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }


}


