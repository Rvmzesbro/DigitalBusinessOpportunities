using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models
{
    public partial class Nomenclatures
    {
        public override string ToString()
        {
            return Name;
        }
        public decimal FifoCost
        {
            get
            {
               
                decimal totalOut = 0;

                if (this.CompositionSales != null)
                    totalOut += this.CompositionSales.Sum(s => s.Count.GetValueOrDefault());

                if (this.MaterialProductions != null)
                    totalOut += this.MaterialProductions.Sum(p => p.Count.GetValueOrDefault());

             
                if (this.CompositionOrderMaterials == null || !this.CompositionOrderMaterials.Any())
                    return 0;

           
                var supplies = this.CompositionOrderMaterials
                    .Where(c => c.OrderPurchaseMaterials != null && c.Count > 0)
                    .OrderBy(c => c.OrderPurchaseMaterials.Date) // Сортировка от старых к новым
                    .ToList();

                decimal currentSupplyAccumulator = 0;

                
                foreach (var supply in supplies)
                {
                    decimal supplyCount = supply.Count.GetValueOrDefault();


                    currentSupplyAccumulator += supplyCount;

               
                    if (currentSupplyAccumulator > totalOut)
                    {
                        return supply.Price.GetValueOrDefault();
                    }
                }

              
                var lastSupply = supplies.LastOrDefault();
                return lastSupply != null ? lastSupply.Price.GetValueOrDefault() : 0;
            }
        }
    }
}
