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
            DGMaterialProduction.ItemsSource = App.db.MaterialProductions.ToList();

            DGWorkType.ItemsSource = App.db.WorkTypes.ToList();
            DGCompositionTransferWarehouse.ItemsSource = App.db.CompositionTransfers.ToList();
            DGTransferWarehouse.ItemsSource = App.db.TransferWarehouses.ToList();
            DGBalanceWarehouse.ItemsSource = App.db.WarehouseBalances.ToList();
            DGWareHouse.ItemsSource = App.db.Warehouses.ToList();
            DGLaborSpecification.ItemsSource = App.db.LaborSpecifications.ToList();
            DGMaterialSpecification.ItemsSource = App.db.MaterialSpecifications.ToList();
            DGStageSpecification.ItemsSource = App.db.StageSpecifications.ToList();
            DGSpecification.ItemsSource = App.db.Specifications.ToList();
            DGEquipment.ItemsSource = App.db.Equipments.ToList();
            DGWorkshop.ItemsSource = App.db.Workshops.ToList();
            DGCompositionBrigade.ItemsSource = App.db.CompositionBrigades.ToList();
            DGWorker.ItemsSource = App.db.Workers.ToList();
            DGBrigade.ItemsSource = App.db.Brigades.ToList();
            DGNomenclature.ItemsSource = App.db.Nomenclatures.ToList();
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
           
            if (TIMaterialProduction.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditMaterialProduction(null));
            }



            if (TINomeclature.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditNomenclature(null));
            }
            if (TIBrigade.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditBrigade(null));
            }
            if (TIWorkType.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditWorkType(null));
            }
            if (TICompositionTransferWarehouse.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditCompositionTransferWarehouse(null));
            }
            if (TITransferWarehouse.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditTransferWarehouse(null));
            }
            if (TIBalanceWarehouse.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditBalanceWarehouse(null));
            }
            if (TIWarehouse.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditWarehouse(null));
            }
            if (TILaborSpecification.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditLaborSpecification(null));
            }
            if (TIMaterialSpecification.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditMaterialSpecification(null));
            }
            if (TIStageSpecification.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditStageSpecification(null));
            }
            if (TISpecification.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditSpecification(null));
            }
            if (TIEquipment.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditEquipment(null));
            }
            if (TIWorkshop.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditWorkshop(null));
            }
            if (TICompositionBtigade.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditCompositionBtigade(null));
            }
            if (TIWorker.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditWorker(null));
            }
            

            else
            {
                return;
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

      

        private void DGMaterialProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DGLaborProduction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

     

        private void BTAddStage_Click(object sender, RoutedEventArgs e)
        {
            if (TIOrderProduction.IsSelected)
            {
                NavigationService.Navigate(new Pages.AddEditStageProduction(null));
            }
        }

        private void DGNomenclature_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGNomenclature.SelectedItem is Nomenclatures nomenclatures)
            {
                NavigationService.Navigate(new Pages.AddEditNomenclature(nomenclatures));
            }
        }

        private void DGBrigade_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGBrigade.SelectedItem is Brigades order)
            {
                NavigationService.Navigate(new Pages.AddEditBrigade(order));
            }
        }

        private void DGWorker_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGWorker.SelectedItem is Workers order)
            {
                NavigationService.Navigate(new Pages.AddEditWorker(order));
            }
        }

        private void DGCompositionBrigade_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGCompositionBrigade.SelectedItem is CompositionBrigades order)
            {
                NavigationService.Navigate(new Pages.AddEditCompositionBtigade(order));
            }
        }

        private void DGWorkshop_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGWorkshop.SelectedItem is Workshops order)
            {
                NavigationService.Navigate(new Pages.AddEditWorkshop(order));
            }
        }

        private void DGEquipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGEquipment.SelectedItem is Equipments order)
            {
                NavigationService.Navigate(new Pages.AddEditEquipment(order));
            }
        }

        private void DGSpecification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSpecification.SelectedItem is Specifications order)
            {
                NavigationService.Navigate(new Pages.AddEditSpecification(order));
            }
        }

        private void DGStageSpecification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGStageSpecification.SelectedItem is StageSpecifications order)
            {
                NavigationService.Navigate(new Pages.AddEditStageSpecification(order));
            }
        }

        private void DGMaterialSpecification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGMaterialSpecification.SelectedItem is MaterialSpecifications order)
            {
                NavigationService.Navigate(new Pages.AddEditMaterialSpecification(order));
            }
        }

        private void DGLaborSpecification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSales.SelectedItem is Sales order)
            {
                NavigationService.Navigate(new Pages.CompositiomSalesOrder(order));
            }
        }

        private void DGWareHouse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGWareHouse.SelectedItem is Warehouses order)
            {
                NavigationService.Navigate(new Pages.AddEditWarehouse(order));
            }
        }

        private void DGBalanceWarehouse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSales.SelectedItem is Sales order)
            {
                NavigationService.Navigate(new Pages.CompositiomSalesOrder(order));
            }
        }

        private void DGTransferWarehouse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSales.SelectedItem is Sales order)
            {
                NavigationService.Navigate(new Pages.CompositiomSalesOrder(order));
            }
        }

        private void DGCompositionTransferWarehouse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGSales.SelectedItem is Sales order)
            {
                NavigationService.Navigate(new Pages.CompositiomSalesOrder(order));
            }
        }

        private void DGWorkType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGWorkType.SelectedItem is WorkTypes order)
            {
                NavigationService.Navigate(new Pages.AddEditWorkType(order));
            }
        }
    }
}
