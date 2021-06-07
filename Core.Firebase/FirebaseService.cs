using Core.Cache;
using Core.Firebase.Assets.Model;
using Core.Firebase.Auth.Model;
using Core.Firebase.Model;
using Firebase.Auth;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Firebase
{
    public class FirebaseService
    {
        public async void GetStocks()
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var query = db.Collection("Stocks").OrderBy("code");
            var snapshots = await query.GetSnapshotAsync();
            await db.Collection("Constants").Document("AppFetch").UpdateAsync(new Dictionary<string, object> { { "appFetched", false } });
            List<StockFirebaseModel> stocks = snapshots.Documents.ToList().Select(document => document.ConvertTo<StockFirebaseModel>()).ToList();
            StocksCache.Shared.CachedStocks = stocks.Select(s => s.GetStockCacheModel()).ToList();
        }

        public async Task<FirebaseAuthLink> SignUp(string email, string password)
        {
            var auth = FirebaseHelper.Shared.Auth;
            var response = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            return response;
        }

        public async Task<FirebaseAuthLink> SignIn(string email, string password)
        {
            var auth = FirebaseHelper.Shared.Auth;
            var response = await auth.SignInWithEmailAndPasswordAsync(email, password);
            return response;
        }
       
        public async Task<List<PortfolioFirebaseModel>> GetUserPortfolioList(UserFirebaseModel userFirebaseModel)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var portfolioRef = db.Collection("Users").Document(userFirebaseModel.ID).Collection("Portfolio");
            var portfolioStockSnapshot = await portfolioRef.GetSnapshotAsync();
            List<PortfolioFirebaseModel> portfolioStockList = portfolioStockSnapshot.Documents.ToList().Select(doc => doc.ConvertTo<PortfolioFirebaseModel>()).ToList();
            return portfolioStockList;
        }
    }
}
