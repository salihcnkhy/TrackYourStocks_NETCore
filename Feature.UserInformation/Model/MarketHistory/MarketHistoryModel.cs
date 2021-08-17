
using System;

namespace Feature.UserInformation.Model
{
    public class MarketHistoryModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public int ProcessType { get; set; }
        public string TriggerPrice { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
