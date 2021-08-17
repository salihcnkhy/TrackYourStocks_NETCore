using Cache.Stocks;
using Core.Cache;
using Core.Firebase.Assets.Model;
using Core.Firebase.Auth.Model;
using Core.Firebase.Enum;
using Core.Firebase.Model;
using Firebase.Auth;
using Firebase.Service.Models;
using Firebase.Service.Models.Alarms;
using Firebase.Service.Models.MarketHistory;
using Firebase.Service.Models.Notification;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Firebase
{
    public class FirebaseService
    {

        #region Stock
        public async void FetchStocks()
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var query = db.Collection(FirestoreCollection.Stokcs.Value()).OrderBy("code");
            var snapshots = await query.GetSnapshotAsync();

            List<StockFirebaseModel> stocks = (await Task.WhenAll(snapshots.Documents.ToList()
                .Select(async document =>
                {
                    var stockModel = document.ConvertTo<StockFirebaseModel>();
                    var daySnapshots = await document.Reference.Collection(FirestoreCollection.Days.Value()).OrderByDescending("day").Limit(20).GetSnapshotAsync();
                    stockModel.StockDayFirebaseModelList = daySnapshots.Documents.Select(dayDocument => dayDocument.ConvertTo<StockDayFirebaseModel>()).ToList();
                    return stockModel;
                }))).Where(result => result != null).ToList();
            var appFetchSnapshot = await db.Collection(FirestoreCollection.Constants.Value()).Document("AppFetch").GetSnapshotAsync();
          
            FirebaseHelper.Shared.FirebaseDateStr = appFetchSnapshot.GetValue<string>("currentDay");
            FirebaseHelper.Shared.AvailableDates = appFetchSnapshot.GetValue<List<string>>("available_date_list").Select(d => DateTime.Parse(d)).ToList();
 
            StocksCache.Shared.CachedStocks = stocks.Select(s => s.GetStockCacheModel()).ToList();
            await appFetchSnapshot.Reference.UpdateAsync(new Dictionary<string, object> { { "appFetched", false } });
        }

        public async Task<StockDayFirebaseModel> GetStockDayInformation(StockDayInformationRequest request)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var query = db.Collection(FirestoreCollection.Stokcs.Value()).Document(request.Code).Collection(FirestoreCollection.Days.Value()).Document(request.Date);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.ConvertTo<StockDayFirebaseModel>();
        }
        public async Task<List<StockCacheModel>> GetCachedStocks(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            return StocksCache.Shared.CachedStocks;
        }
        #endregion

        #region Auth


        public async Task<UserFirebaseModel> SignUp(string email, string password)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var auth = FirebaseHelper.Shared.Auth;

            var response = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            var generatedToken = Guid.NewGuid().ToString();
            // Token Update
            var userMap = new Dictionary<string, object> { { "id", response.User.LocalId }, { "lastSignedToken", generatedToken } };
            // User Creation into Firestore
            await db.Collection(FirestoreCollection.Users.Value()).Document(response.User.LocalId).SetAsync(userMap);
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
            await db.Collection(FirestoreCollection.Users.Value()).Document(response.User.LocalId).UpdateAsync(userMap);

            return new UserFirebaseModel() { ID = response.User.LocalId, LastSignedToken = generatedToken };
        }

        public async Task<List<PortfolioFirebaseModel>> GetUserPortfolioList(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);

            FirestoreDb db = FirebaseHelper.Shared.Db;
            var portfolioRef = db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).Collection(FirestoreCollection.Portfolio.Value());
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
        private async Task CheckUserSignToken(FirestoreGeneralRequest request)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var userSnapshot = await db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).GetSnapshotAsync();
            var userFirebaseModel = userSnapshot.ConvertTo<UserFirebaseModel>();
            if (userFirebaseModel == null) throw new Exception(); // TODO: Throw UserNotFound Exception
            if (!userFirebaseModel.LastSignedToken.Equals(request.UserToken)) throw new Exception(); // TODO: Throw TokenUnvalid Exception
        }
        #endregion

        #region User

        public async Task<List<string>> GetFavoriteStockCodes(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            var db = FirebaseHelper.Shared.Db;

            var query = db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID);
            var snapshot = await query.GetSnapshotAsync();
            List<string> favoriteStocks = snapshot.GetValue<List<string>>("favorite_stoks");
            return favoriteStocks;
        }
        public async Task<List<AlarmFirebaseModel>> GetActiveAlarms(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            var db = FirebaseHelper.Shared.Db;

            var query = db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).Collection(FirestoreCollection.Alarms.Value());
            var alarmSnapshots = await query.GetSnapshotAsync();

            var alarms = alarmSnapshots.Select(s => s.ConvertTo<AlarmFirebaseModel>()).ToList();
            return alarms;
        }

        public async Task<List<NotificationFirebaseModel>> GetNotifications(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            var db = FirebaseHelper.Shared.Db;
            var query = db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).Collection(FirestoreCollection.Notifications.Value());
            var notificationSnapshots = await query.GetSnapshotAsync();

            var notifications = notificationSnapshots.Select(s => s.ConvertTo<NotificationFirebaseModel>()).ToList();
            return notifications;
        }

        public async Task<List<MarketHistoryFirebaseModel>> GetMarketHistory(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            var db = FirebaseHelper.Shared.Db;
            var query = db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).Collection(FirestoreCollection.MarketHistory.Value());
            var marketHistorySnapshots = await query.GetSnapshotAsync();

            var marketHistoryList = marketHistorySnapshots.Select(s => s.ConvertTo<MarketHistoryFirebaseModel>()).ToList();
            return marketHistoryList;
        }

        #endregion
    }
}
