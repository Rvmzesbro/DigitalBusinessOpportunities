using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models
{
    public partial class CompositionSales
    {
        public string GetSum
        {
            get
            {
                return (Price * Count).ToString();
            }
        }
    }
}
