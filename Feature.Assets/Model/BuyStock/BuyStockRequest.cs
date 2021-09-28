using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model
{
    public class BuyStockRequest : AuthRequiredRequest
    {
        public string Code { get; set; }
        public int LotQuantity { get; set; }
        public double BuyPrice { get; set; }
    }
}
