using Domain.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model
{
    public class AssetStockInformationModel
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double BoughtPrice { get; set; }  // UnitPrice * StockQuantity
        public double ProfitRate { get; set; }
        public double ProfitValue { get; set; }
        public double RateOfStockPrice { get; set; } // Ratio by Total Asset Current Value
        public double TotalValue { get; set; }
    }
}
