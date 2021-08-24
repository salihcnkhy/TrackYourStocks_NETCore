using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{

    [FirestoreData]
    public class AlarmFirebaseModel
    {

        [FirestoreProperty("id")]
        public string Id { get; set; }

        [FirestoreProperty("code")]
        public string Code { get; set; }

        [FirestoreProperty("long_name")]
        public string LongName { get; set; }

        [FirestoreProperty("condition_type")]
        public int ConditionType { get; set; }

        [FirestoreProperty("trigger_price")]
        public double TriggerPrice { get; set; }
    }
}
