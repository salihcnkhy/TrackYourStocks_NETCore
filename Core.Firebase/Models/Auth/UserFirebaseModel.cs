using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{
    [FirestoreData]
    public class UserFirebaseModel
    {
        [FirestoreProperty("id")]
        public string ID { get; set; }

        [FirestoreProperty("username")]
        public string Username { get; set; }

        [FirestoreProperty("lastSignedToken")]
        public string LastSignedToken { get; set; }

        [FirestoreProperty("favorite_stocks")]
        public List<string> FavoriteStocks { get; set; }

        public DocumentReference UserReference { get; set; }
    }
}
