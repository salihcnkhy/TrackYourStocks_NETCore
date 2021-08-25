using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStocksServiceResponse
    {
        public bool Success { get; set; }
        public List<GetStocksServiceValueObject> ValueObjects { get; set; }
        public bool IsContinue { get; set; }
        public string ClientUpdateUUID { get; set; }
    }
}
