using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models.Notification
{
    [FirestoreData]
    public class NotificationFirebaseModel
    {

        [FirestoreProperty("id")]
        public string Id { get; set; }

        [FirestoreProperty("body")]
        public string Body { get; set; }

        [FirestoreProperty("title")]
        public string Title { get; set; }

        [FirestoreProperty("has_shown")]
        public bool HasShown { get; set; }

        [FirestoreProperty("date")]
        public Timestamp Date { get; set; }

        [FirestoreProperty("ic_name")]
        public string IconName { get; set; }
    }
}
