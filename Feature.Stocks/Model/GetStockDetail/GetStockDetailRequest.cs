using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Model
{
    public class GetStockDetailRequest : AuthRequiredRequest
    {
        public bool IsProfitDayInfomationRequired { get; set; } = false;
        public string Code { get; set; }
        public int DayFrequency { get; set; } = 1;
        public int DayInformationSize { get; set; } = 20;
    }
}
