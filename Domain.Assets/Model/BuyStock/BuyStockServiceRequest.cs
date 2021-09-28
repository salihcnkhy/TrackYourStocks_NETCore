using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Model
{
    public class BuyStockServiceRequest : AuthRequiredRequest 
    {
        public string Code { get; set; }
        public int AdditionalBuyQuantity { get; set; }
        public double CurrentBoughtPrice { get; set; }
        public double NewUnitBoughtPrice { get; set; }
    }
}
