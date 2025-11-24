using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models
{
    partial class CompositionOrderMaterials
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
