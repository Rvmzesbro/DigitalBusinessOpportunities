using DigitalBusinessOpportunities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для AddEditNomenclature.xaml
    /// </summary>
    public partial class AddEditNomenclature : Page
    {
        public Nomenclatures material;
        public List<string> Types = new List<string>()
        {
            "сырье",
            "полуфабрикат",
            "готовая продукция"
        };
        public AddEditNomenclature(Nomenclatures compositionOrderMaterials)
        {
            InitializeComponent();
            Bindings();
            material = compositionOrderMaterials;
            DataContext = material;
        }

        private void Bindings()
        {
            CBType.ItemsSource = Types;
        }

        private void BTAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) || string.IsNullOrWhiteSpace(TBUnit.Text) || CBType.SelectedItem == null || string.IsNullOrWhiteSpace(TBArticle.Text))
            {
                return;
            }
            else
            {
                

                if (material == null)
                {
                    if (!long.TryParse(TBArticle.Text, out long result)) return;
                    bool IsArticle = App.db.Nomenclatures.Any(p => p.Article == result);
                    if (IsArticle)
                    {
                        return;
                    }
                        var newComposition = new Nomenclatures()
                    {
                        Name = TBName.Text,
                        Type = CBType.SelectedItem.ToString(),
                        Article = long.Parse(TBArticle.Text),
                        Unit = TBUnit.Text

                    };

                    App.db.Nomenclatures.Add(newComposition);
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
