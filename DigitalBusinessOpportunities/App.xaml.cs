using DigitalBusinessOpportunities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace DigitalBusinessOpportunities
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWindow;
        public static DigitalBusinessOpportunitiesEntities db { get; set; } = new DigitalBusinessOpportunitiesEntities();
    }
}
