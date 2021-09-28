using Cache.Stocks;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStockDayInfoServiceResponse
    {
        public double LastBuying { get; set; }
        public double LastSelling { get; set; }
        public string Day { get; set; }
    }
}
