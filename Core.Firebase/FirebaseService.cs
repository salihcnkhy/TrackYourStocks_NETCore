using Core.Cache;
using Core.Firebase.Assets.Model;
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
       
        public async Task<PortfolioFirebaseModel> GetMinimizedAssets()
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var portfolioRef = db.Collection("Users").Document("tWYCTN7chrHE4pmzDCDB").Collection("Portfolio");
            var portfolioStockSnapshot = await portfolioRef.GetSnapshotAsync();
            List<PortfolioFirebaseModel> portfolioStockList = portfolioStockSnapshot.Documents.ToList().Select(doc => doc.ConvertTo<PortfolioFirebaseModel>()).ToList();

            // Current Asset
            double totalCurrentAsset = 0;
            double totalBoughtPrice = 0;
            foreach(var portfolioStock in portfolioStockList)
            {
                var stockCode = portfolioStock.Code;
                var portfolioStockQuantity = portfolioStock.StockQuantity;

                var stock = StocksCache.Shared.CachedStocks.First(stock => stock.Code.Equals(stockCode));
                totalCurrentAsset += portfolioStockQuantity * stock.CurrentSelling;
                totalBoughtPrice += portfolioStockQuantity * portfolioStock.UnitPrice;
            }

            // Current Profit
            var currentProfit = totalCurrentAsset - totalBoughtPrice;

            // current profit rate
            double currentProfitRate = 100 * (totalCurrentAsset - totalBoughtPrice) / totalBoughtPrice;

            // for model strings
            string totalCurrentAssetStr = String.Format("{0:0.00} TL", totalCurrentAsset);
            string currentProfitStr = String.Format("{0:0.00} TL", currentProfit);
            string currentProtiftRateStr = String.Format("%{0:0.00}", currentProfitRate);

            return null;
        }
    }
}
