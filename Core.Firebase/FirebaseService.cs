using Core.Cache;
using Core.Firebase.Assets.Model;
using Core.Firebase.Auth.Model;
using Core.Firebase.Enum;
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

        public async Task<UserFirebaseModel> SignUp(string email, string password)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var auth = FirebaseHelper.Shared.Auth;

            var response = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            var generatedToken = Guid.NewGuid().ToString();
            // Token Update
            var userMap = new Dictionary<string, object> { { "id", response.User.LocalId }, { "lastSignedToken", generatedToken } };
            // User Creation into Firestore
            await db.Collection("Users").Document(response.User.LocalId).SetAsync(userMap);
            return new UserFirebaseModel() { ID = response.User.LocalId, LastSignedToken = generatedToken };
        }

        public async Task<UserFirebaseModel> SignIn(string email, string password)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var auth = FirebaseHelper.Shared.Auth;

            var response = await auth.SignInWithEmailAndPasswordAsync(email, password);

            var generatedToken = Guid.NewGuid().ToString();
            // Token update
            var userMap = new Dictionary<string, object> { { "lastSignedToken", generatedToken } };
            await db.Collection("Users").Document(response.User.LocalId).UpdateAsync(userMap);

            return new UserFirebaseModel() { ID = response.User.LocalId, LastSignedToken = generatedToken };
        }

        public async Task<List<PortfolioFirebaseModel>> GetUserPortfolioList(string userID, string userToken)
        {
            var userTokenCheck = await CheckUserSignToken(userID, userToken);
            if (!userTokenCheck) return null;

            FirestoreDb db = FirebaseHelper.Shared.Db;
            var portfolioRef = db.Collection("Users").Document(userID).Collection("Portfolio");
            var portfolioStockSnapshot = await portfolioRef.GetSnapshotAsync();
            List<PortfolioFirebaseModel> portfolioStockList = portfolioStockSnapshot.Documents.ToList().Select(doc => doc.ConvertTo<PortfolioFirebaseModel>()).ToList();
            return portfolioStockList;
        }

        /// <summary>
        /// Checks Last Signed User Token is Same
        /// </summary>
        /// <param name="userFirebaseModel">
        /// Pass => ID & LastSignedToken
        /// </param>
        /// <returns></returns>
        public async Task<bool> CheckUserSignToken(string userID, string userToken)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;

            var userSnapshot = await db.Collection(FirestoreCollection.Users.Value()).Document(userID).GetSnapshotAsync();
            var userFirebaseModel = userSnapshot.ConvertTo<UserFirebaseModel>();
            if (userFirebaseModel == null) return false;
            return userFirebaseModel.LastSignedToken.Equals(userToken);
        }
    }
}
