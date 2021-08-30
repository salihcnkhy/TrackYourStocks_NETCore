
using System;

namespace Domain.Assets.Model
{
    public class MarketHistoryServiceModel
    {
        public string Code { get; set; }
        public string LongName { get; set; }
        public string ProcessType { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
