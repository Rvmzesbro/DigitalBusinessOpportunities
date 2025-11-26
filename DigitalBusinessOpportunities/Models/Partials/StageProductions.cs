using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models
{
    public partial class StageProductions
    {
        public override string ToString()
        {
            return CompositionProductions.Nomenclatures.Name;
        }
        public string GetStageId
        {
            get
            {
                return Id.ToString();
            }
        }
    }
}
