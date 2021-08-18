using Firebase.Auth;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Firebase
{
    public class FirebaseHelper
    {
        private static FirebaseHelper _shared;

        private bool IsFirstRequest = true;
        public FirestoreDb Db;
        public FirebaseAuthProvider Auth;
        public string FirebaseDateStr;
        public List<DateTime> AvailableDates;
        public static FirebaseHelper Shared
        {
            get
            {
                if (_shared == null)
                {
                    _shared = new FirebaseHelper();
                }
                return _shared;
            }
        }
        private FirebaseHelper()
        {
            Db = FirestoreDb.Create("hesabinibilapplication");
            Auth =  new FirebaseAuthProvider(new FirebaseConfig("AIzaSyChMSHcWx1t2To0e1OmxMjjf7ZClAUbR_4"));
        }

        public void AddListenerToFirestoreFetched()
        {
            DocumentReference docRef = Db.Collection("Constants").Document("AppFetch");

            FirestoreChangeListener listener = docRef.Listen(async snapshot =>
            {
                if (snapshot.Exists)
                {
                    Dictionary<string, object> dict = snapshot.ToDictionary();
                    bool appFetched = (bool)dict["appFetched"];
                    if (appFetched || IsFirstRequest)
                    {
                        await snapshot.Reference.UpdateAsync(new Dictionary<string, object> { { "appFetched", false } });
                        Console.WriteLine("Cache Fetched");
                        IsFirstRequest = false;
                        var service = new FirebaseService();
                        service.FetchStocks();
                    }
                }
            });
        }
    }
}
