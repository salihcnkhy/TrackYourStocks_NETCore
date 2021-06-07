using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Firebase.Auth.Model
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
    }
}
