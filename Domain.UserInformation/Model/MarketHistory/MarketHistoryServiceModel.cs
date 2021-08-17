
using System;

namespace Domain.UserInformation.Model
{
    public class MarketHistoryServiceModel
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
