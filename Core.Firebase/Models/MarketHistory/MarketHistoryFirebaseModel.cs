using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{
    [FirestoreData]
    public class MarketHistoryFirebaseModel
    {

        [FirestoreProperty("code")]
        public string Code { get; set; }

        [FirestoreProperty("long_name")]
        public string LongName { get; set; }

        [FirestoreProperty("process_type")]
        public string ProcessType { get; set; }

        [FirestoreProperty("date")]
        public Timestamp Date { get; set; }

        [FirestoreProperty("quantity")]
        public int Quantity { get; set; }

        [FirestoreProperty("unit_price")]
        public double UnitPrice { get; set; }
    }
}
