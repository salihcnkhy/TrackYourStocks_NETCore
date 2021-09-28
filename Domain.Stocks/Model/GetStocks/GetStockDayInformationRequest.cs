using Cache.Stocks;
using Core.Base;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStockDayInfoServiceRequest
    {
        public string Code { get; set; }
        public string Date { get; set; }
    }
}
