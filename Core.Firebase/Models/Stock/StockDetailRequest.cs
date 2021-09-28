using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{
    public class StockDetailRequest : FirestoreGeneralRequest
    {
        public DocumentSnapshot StockSnapshot { get; set; }
        public bool IsProfitDayInfomationRequired { get; set; } = false;
        public string Code { get; set; }
        public int DayFrequency { get; set; }
        public int DayInformationSize { get; set; }
    }
}
