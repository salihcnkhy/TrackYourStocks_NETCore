using Cache.Stocks;
using Core.Cache;
using Core.Extensions;
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

            var appFetchSnapshot = await db.Collection(FirestoreCollection.Constants.Value()).Document("AppFetch").GetSnapshotAsync();
            FirebaseHelper.Shared.FirebaseDateStr = appFetchSnapshot.GetValue<string>("currentDay");
            FirebaseHelper.Shared.AvailableDates = appFetchSnapshot.GetValue<List<string>>("available_date_list").Select(d => DateTime.Parse(d)).ToList();

            List<StockFirebaseModel> stocks = (await Task.WhenAll(snapshots.Documents.ToList()
                .Select(async document =>
                {
                    var stockModel = document.ConvertTo<StockFirebaseModel>();
                    var dayRef = document.Reference.Collection(FirestoreCollection.Days.Value());
                    var daySnapshots = await dayRef.OrderByDescending("day").Limit(20).GetSnapshotAsync();

                    var oneWeekBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddDays(-7);
                    var oneMountBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddMonths(-1);
                    var threeMountBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddMonths(-3);

                    var availableDates = FirebaseHelper.Shared.AvailableDates;

                    oneWeekBeforeDate = oneWeekBeforeDate.GetNearestPastAvailableDate(availableDates);
                    oneMountBeforeDate = oneMountBeforeDate.GetNearestPastAvailableDate(availableDates);
                    threeMountBeforeDate = threeMountBeforeDate.GetNearestPastAvailableDate(availableDates);

                    var oneWeekBeforeRequest = new StockDayInformationRequest
                    {
                        Reference = dayRef,
                        Date = oneWeekBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var oneMounthBeforeRequest = new StockDayInformationRequest
                    {
                        Reference = dayRef,
                        Date = oneMountBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var threeMounthBeforeRequest = new StockDayInformationRequest
                    {
                        Reference = dayRef,
                        Date = oneMountBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var oneWeekBeforeDayInfo = await GetStockDayInformation(oneWeekBeforeRequest);
                    var oneMounthBeforeDayInfo = await GetStockDayInformation(oneMounthBeforeRequest);
                    var threeMounthBeforeDayInfo = await GetStockDayInformation(threeMounthBeforeRequest);

                    var oneWeekBeforeDayProfit = stockModel.CurrentSelling - oneWeekBeforeDayInfo.LastSelling;
                    var oneWeekBeforeDayProfitRate = (100 * oneWeekBeforeDayProfit) / (oneWeekBeforeDayInfo.LastSelling == 0 ? 1.0 : oneWeekBeforeDayInfo.LastSelling);

                    var oneMounthBeforeDayProfit = stockModel.CurrentSelling - oneMounthBeforeDayInfo.LastSelling;
                    var oneMounthBeforeDayProfitRate = (100 * oneMounthBeforeDayProfit) / (oneMounthBeforeDayInfo.LastSelling == 0 ? 1.0 : oneMounthBeforeDayInfo.LastSelling);

                    var threeMounthBeforeDayProfit = stockModel.CurrentSelling - threeMounthBeforeDayInfo.LastSelling;
                    var threeMounthBeforeDayProfitRate = (100 * threeMounthBeforeDayProfit) / (threeMounthBeforeDayInfo.LastSelling == 0 ? 1.0 : threeMounthBeforeDayInfo.LastSelling);

                    stockModel.StockProfitDayFirebaseModel = new List<StockProfitDayModel>
                    {
                        new StockProfitDayModel
                        {
                            Title = "Bugün",
                            ProtifRate = stockModel.CurrentChangeRate,
                            Protif = stockModel.CurrentChange
                        },

                        new StockProfitDayModel
                        {
                            Title = "1 Hafta",
                            ProtifRate = stockModel.CurrentChangeRate,
                        },

                        new StockProfitDayModel
                        {
                            Title = "1 Ay",
                            ProtifRate = stockModel.CurrentChangeRate,
                        },

                        new StockProfitDayModel
                        {
                            Title = "3 Ay",
                            ProtifRate = stockModel.CurrentChangeRate,
                        },
                    };

                    stockModel.StockDayFirebaseModelList = daySnapshots.Documents.Select(dayDocument => dayDocument.ConvertTo<StockDayFirebaseModel>()).ToList();
                    return stockModel;
                }))).Where(result => result != null).ToList();

            StocksCache.Shared.CachedStocks = stocks.Select(s => s.GetStockCacheModel()).ToList();
        }

        public async Task<StockDayFirebaseModel> GetStockDayInformation(StockDayInformationRequest request)
        {
            var dayRef = request.Reference;
            if (dayRef == null)
            {
                FirestoreDb db = FirebaseHelper.Shared.Db;
                dayRef = db.Collection(FirestoreCollection.Stokcs.Value()).Document(request.Code).Collection(FirestoreCollection.Days.Value());
            }
            var query = dayRef.Document(request.Date);
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
