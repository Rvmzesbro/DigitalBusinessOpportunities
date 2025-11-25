using DigitalBusinessOpportunities.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            DGOrder.ItemsSource = App.db.OrderPurchaseMaterials.ToList();
            DGCompositionSales.ItemsSource = App.db.CompositionSales.Where(p => p.SaleId == null).ToList();
            DGSales.ItemsSource = App.db.Sales.ToList();
            DGProducts.ItemsSource = App.db.CompositionProductions.Where(p => p.OrderId == null).ToList();
            DGOrderProduction.ItemsSource = App.db.OrderProductions.Where(p => p.Status != "завершен").ToList();
            DGStageProduction.ItemsSource = App.db.StageProductions.Where(p => p.Status != "завершен").ToList();
        }

        private void BTBuy_Click(object sender, RoutedEventArgs e)
        {
            if (TIPurchaseMaterial.IsSelected)
            {

                var compositions = App.db.CompositionOrderMaterials.Where(p => p.OrderId == null).ToList();
                if (compositions.Count != 0)
                {
                    var newOrder = new OrderPurchaseMaterials()
                    {
                        Date = DateTime.Now,
                    };

                    App.db.OrderPurchaseMaterials.Add(newOrder);
                    App.db.SaveChanges();

                    foreach (var composition in compositions)
                    {
                        composition.OrderId = newOrder.Id;


                        var warehouseBalance = App.db.WarehouseBalances.FirstOrDefault(x => x.WarehouseId == composition.WarehouseId && x.NomenclatureId == composition.NomenclatureId);
                        if (warehouseBalance == null)
                        {
                            App.db.WarehouseBalances.Add(
                            new WarehouseBalances()
                            {
                                WarehouseId = composition.WarehouseId,
                                NomenclatureId = composition.NomenclatureId,
                                Count = composition.Count
                            });

                        }
                        else { 
                        
                            warehouseBalance.Count += composition.Count;
                        }
                        
                    }
                    App.db.SaveChanges();
                    Refresh();
                }

            }

            if (TISalesNomenclature.IsSelected)
            {

                var compositions = App.db.CompositionSales.Where(p => p.SaleId == null).ToList();
               
                if (compositions.Count != 0)
                {
                    foreach(var item  in compositions)
                    {
                        var warehouseBalance = App.db.WarehouseBalances.FirstOrDefault(p => p.WarehouseId == item.WarehouseId && p.NomenclatureId == item.NomenclatureId);
                        if(warehouseBalance.Count < item.Count)
                        {
                            return;
                        }
                    }
                        var newOrder = new Sales()
                    {
                        Date = DateTime.Now,
                    };

                    App.db.Sales.Add(newOrder);
                    App.db.SaveChanges();

                    foreach (var composition in compositions)
                    {
                        composition.SaleId = newOrder.Id;

                        var warehouseBalance = App.db.WarehouseBalances.FirstOrDefault(p => p.WarehouseId == composition.WarehouseId && p.NomenclatureId == composition.NomenclatureId);

                        warehouseBalance.Count -= composition.Count;
                    }
                    App.db.SaveChanges();
                    Refresh();
                }

            }

            if (TIProductsProduction.IsSelected)
            {
                var compositions = App.db.CompositionProductions.Where(p => p.OrderId == null).ToList();

                if (compositions.Count != 0)
                {
                    var newOrder = new OrderProductions()
                    {
                        Date = DateTime.Now,
                        Status = "в процессе"
                    };
                    App.db.OrderProductions.Add(newOrder);
                    App.db.SaveChanges(); 
                    

                    foreach (var composition in compositions)
                    {
                        composition.OrderId = newOrder.Id;
                    }
                    App.db.SaveChanges();
                    Refresh();
                }
            }

        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TIPurchaseMaterial.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditMaterials(null));
            }

            if (TISalesNomenclature.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditSales(null));
            }
            if (TIProductsProduction.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditProduction(null));
            }
            if (TIStageProduction.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditStageProduction(null));
            }
        }

        private void BTRemove_Click(object sender, RoutedEventArgs e)
        {
            if (TIPurchaseMaterial.IsSelected)
            {
                if (DGPurchase.SelectedItem is CompositionOrderMaterials material)
                {
                    App.db.CompositionOrderMaterials.Remove(material);
                    App.db.SaveChanges();
                    Refresh();
                }
            }
            if (TISalesNomenclature.IsSelected)
            {
                if (DGCompositionSales.SelectedItem is CompositionSales product)
                {
                    App.db.CompositionSales.Remove(product);
                    App.db.SaveChanges();
                    Refresh();
                }
            }
            if (TIProductsProduction.IsSelected)
            {
                if (DGProducts.SelectedItem is CompositionProductions product)
                {
                    App.db.CompositionProductions.Remove(product);
                    App.db.SaveChanges();
                    Refresh();
                }
            }
        }

        private void DGPurchase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGPurchase.SelectedItem is CompositionOrderMaterials material)
            {
                NavigationService.Navigate(new Pages.AddEditMaterials(material));
            }
        }

        private void DGOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGOrder.SelectedItem is OrderPurchaseMaterials order)
            {
                NavigationService.Navigate(new Pages.CompositionOrder(order));
            }
        }


        private void DGCompositionSales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGCompositionSales.SelectedItem is CompositionSales sales)
            {
                NavigationService.Navigate(new Pages.AddEditSales(sales));
            }
        }

        private void DGSales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSales.SelectedItem is Sales order)
            {
                NavigationService.Navigate(new Pages.CompositiomSalesOrder(order));
            }
        }

        private void DGProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(DGProducts.SelectedItem is CompositionProductions product)
            {
                NavigationService.Navigate(new Pages.AddEditProduction(product));
            }
        }

        private void DGOrderProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGOrderProduction.SelectedItem is OrderProductions product)
            {
                NavigationService.Navigate(new Pages.CompositionProductProduction(product));
            }
        }

        private void DGStageProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DGMaterialProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DGLaborProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
