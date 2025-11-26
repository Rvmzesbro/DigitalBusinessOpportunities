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
    /// Логика взаимодействия для AddEditStageProduction.xaml
    /// </summary>
    public partial class AddEditStageProduction : Page
    {
        public StageProductions stage;
        public List<string> Statuses = new List<string>()
        {
            "в процессе",
            "завершен"
        };
        public AddEditStageProduction(StageProductions stageProductions)
        {
            InitializeComponent();
            Bindings();
            stage = stageProductions;
          //  CBStatus.SelectedItem = Statuses.FirstOrDefault(p => p.Equals(stage.Status));
            DataContext = stage;
        }

        private void Bindings()
        {
            CBStatus.ItemsSource = Statuses;
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBStageName.Text) || string.IsNullOrWhiteSpace(TBOrder.Text) || CBNomenclature.SelectedItem == null || string.IsNullOrWhiteSpace(TBStageNumber.Text) || string.IsNullOrWhiteSpace(TBUnit.Text) || string.IsNullOrWhiteSpace(TBCount.Text))
            {
                return;
            }
            else
            {
                if (stage == null)
                {
                    if (!int.TryParse(TBOrder.Text, out int result1)) return;
                    if (!int.TryParse(TBStageNumber.Text, out int result2)) return;
                    if (!decimal.TryParse(TBCount.Text, out decimal result)) return;
                    var nomenclature = CBNomenclature.SelectedItem as CompositionProductions;
                    var NewProduct = new StageProductions()
                    {
                        OrderId = int.Parse(TBOrder.Text),
                        CompositionOrderId = nomenclature.NomenclatureId,
                        Count = decimal.Parse(TBCount.Text),
                        Unit = TBUnit.Text,
                        StageName = TBStageName.Text,
                        StageNumber = int.Parse(TBStageNumber.Text),
                        Status = "в процессе",
                        WarehouseId = null
                    };
                    App.db.StageProductions.Add(NewProduct);
                }


                if(stage != null)
                {
                    if(stage.Status == Statuses[1])
                    {
                        if(stage.OrderProductions.StageProductions.FirstOrDefault(x=>x.Status == Statuses[0])==null)
                            stage.OrderProductions.Status = stage.Status;
                    }
                }

                App.db.SaveChanges();


                NavigationService.GoBack();
            }
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TBOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBOrder.Text))
            {
               CBNomenclature.ItemsSource = null;
                return;
            }
            else
            {
                var orderId = int.Parse(TBOrder.Text);
                CBNomenclature.ItemsSource = App.db.CompositionProductions.Where(p => p.OrderId == orderId && p.OrderId != null).ToList();
            }
        }
    }
}
