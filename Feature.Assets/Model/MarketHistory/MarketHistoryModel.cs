
using System;

namespace Feature.Assets.Model
{
    public class MarketHistoryModel
    {
        public string Code { get; set; }
        public string LongName { get; set; }
        public int ProcessType { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
