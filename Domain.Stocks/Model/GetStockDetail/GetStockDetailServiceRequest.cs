using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStockDetailServiceRequest : AuthRequiredRequest
    {
        public bool IsProfitDayInfomationRequired { get; set; } = false;
        public string Code { get; set; }
        public int DayFrequency { get; set; }
        public int DayInformationSize { get; set; }
    }
}
