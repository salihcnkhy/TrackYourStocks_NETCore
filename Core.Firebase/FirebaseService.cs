using Cache.Stocks;
using Core.Cache;
using Core.Extensions;
using Core.Firebase.Enum;
using Core.Firebase.Model;
using Firebase.Auth;
using Firebase.Service.Models;
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
            FirebaseHelper.Shared.LastUpdateUUID = Guid.NewGuid().ToString();

            List<StockCacheModel> stocks = (await Task.WhenAll(snapshots.Documents.ToList().Select(async document =>
            {
                return await GetStockDetail(new StockDetailRequest { StockSnapshot = document, DayFrequency = 1, DayInformationSize = 20 });

            }))).Where(result => result != null).ToList();

            StocksCache.Shared.CachedStocks = stocks;
        }

        public async Task<StockDayFirebaseModel> GetStockDayInformation(StockDayInformationRequest request)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var dayRef = db.Collection(FirestoreCollection.Stokcs.Value()).Document(request.Code).Collection(FirestoreCollection.Days.Value());
            var query = dayRef.Document(request.Date);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.ConvertTo<StockDayFirebaseModel>();
        }

        public async Task<List<StockCacheModel>> GetCachedStocks(FirestoreGeneralRequest request)
        {
            await CheckUserSignToken(request);
            return StocksCache.Shared.CachedStocks;
        }

        public async Task<StockCacheModel> GetStockDetail(StockDetailRequest request)
        {
            if (request.StockSnapshot == null)
            {
                await CheckUserSignToken(request);
                FirestoreDb db = FirebaseHelper.Shared.Db;
                request.StockSnapshot = await db.Collection(FirestoreCollection.Stokcs.Value()).Document(request.Code).GetSnapshotAsync();
            }

            var stockModel = request.StockSnapshot.ConvertTo<StockFirebaseModel>();
            var dayRef = request.StockSnapshot.Reference.Collection(FirestoreCollection.Days.Value());

            var availableDates = FirebaseHelper.Shared.AvailableDates;
            availableDates.Sort((x, y) => y.CompareTo(x));

            List<string> dates = new List<string>();
            List<List<string>> dateGroups = new List<List<string>>();

            for (int i = 0; i < availableDates.Count; i++)
            {
                if (i % request.DayFrequency == 0)
                {
                    dates.Add(availableDates[i].ToString("yyyy-MM-dd"));
                    if (dates.Count == request.DayInformationSize)
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < dates.Count; i += 10)
            {
                dateGroups.Add(dates.GetRange(i, dates.Count - i < 10 ? dates.Count - i : 10));
            }

            stockModel.StockDayFirebaseModelList = new List<StockDayFirebaseModel>();

            foreach (var days in dateGroups)
            {
                var daySnapshots = await dayRef.WhereIn("day", days).Limit(10).GetSnapshotAsync();
                var stockDayList = daySnapshots.Documents.Select(dayDocument => dayDocument.ConvertTo<StockDayFirebaseModel>()).ToList();
                stockModel.StockDayFirebaseModelList.AddRange(stockDayList);
            }
            stockModel.StockDayFirebaseModelList.Sort((x, y) =>
            {
                return DateTime.Parse(x.Day).CompareTo(DateTime.Parse(y.Day));
            });
            #region StockProfitInformation

            if (request.IsProfitDayInfomationRequired)
            {
                var oneWeekBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddDays(-7);
                var oneMountBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddMonths(-1);
                var threeMountBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddMonths(-3);

                oneWeekBeforeDate = oneWeekBeforeDate.GetNearestPastAvailableDate(availableDates);
                oneMountBeforeDate = oneMountBeforeDate.GetNearestPastAvailableDate(availableDates);
                threeMountBeforeDate = threeMountBeforeDate.GetNearestPastAvailableDate(availableDates);

                var oneWeekBeforeRequest = new StockDayInformationRequest
                {
                    Code = stockModel.Code,
                    Date = oneWeekBeforeDate.ToString("yyyy-MM-dd"),
                };

                var oneMounthBeforeRequest = new StockDayInformationRequest
                {
                    Code = stockModel.Code,
                    Date = oneMountBeforeDate.ToString("yyyy-MM-dd"),
                };

                var threeMounthBeforeRequest = new StockDayInformationRequest
                {
                    Code = stockModel.Code,
                    Date = oneMountBeforeDate.ToString("yyyy-MM-dd"),
                };

                var oneWeekBeforeDayInfo = await GetStockDayInformation(oneWeekBeforeRequest);
                var oneMounthBeforeDayInfo = await GetStockDayInformation(oneMounthBeforeRequest);
                var threeMounthBeforeDayInfo = await GetStockDayInformation(threeMounthBeforeRequest);

                var oneWeekBeforeDayProfit = stockModel.CurrentSelling - oneWeekBeforeDayInfo?.LastSelling ?? stockModel.CurrentSelling;
                var oneWeekBeforeDayProfitRate = (100 * oneWeekBeforeDayProfit) / (oneWeekBeforeDayInfo?.LastSelling == 0 ? 1.0 : oneWeekBeforeDayInfo?.LastSelling ?? 1.0);

                var oneMounthBeforeDayProfit = stockModel.CurrentSelling - (oneMounthBeforeDayInfo?.LastSelling ?? stockModel.CurrentSelling);
                var oneMounthBeforeDayProfitRate = (100 * oneMounthBeforeDayProfit) / (oneMounthBeforeDayInfo?.LastSelling == 0 ? 1.0 : oneMounthBeforeDayInfo?.LastSelling ?? 1.0);

                var threeMounthBeforeDayProfit = stockModel.CurrentSelling - threeMounthBeforeDayInfo?.LastSelling ?? stockModel.CurrentSelling;
                var threeMounthBeforeDayProfitRate = (100 * threeMounthBeforeDayProfit) / (threeMounthBeforeDayInfo?.LastSelling == 0 ? 1.0 : threeMounthBeforeDayInfo?.LastSelling ?? 1.0);

                stockModel.StockProfitDayFirebaseModel = new List<StockProfitDayModel>
                {
                    new StockProfitDayModel
                    {
                        Title = "Bugün",
                        Protif = Math.Round(stockModel.CurrentChange, 2, MidpointRounding.AwayFromZero),
                        ProtifRate = Math.Round(stockModel.CurrentChangeRate, 2, MidpointRounding.AwayFromZero),
                    },
                    new StockProfitDayModel
                    {
                        Title = "1 Hafta",
                        Protif = Math.Round(oneWeekBeforeDayProfit, 2, MidpointRounding.AwayFromZero),
                        ProtifRate = Math.Round(oneWeekBeforeDayProfitRate, 2, MidpointRounding.AwayFromZero),
                    },
                    new StockProfitDayModel
                    {
                        Title = "1 Ay",
                        Protif = Math.Round(oneMounthBeforeDayProfit, 2, MidpointRounding.AwayFromZero),
                        ProtifRate = Math.Round(oneMounthBeforeDayProfitRate, 2,MidpointRounding.AwayFromZero),
                    },
                    new StockProfitDayModel
                    {
                        Title = "3 Ay",
                        Protif = Math.Round(threeMounthBeforeDayProfit, 2, MidpointRounding.AwayFromZero),
                        ProtifRate = Math.Round(threeMounthBeforeDayProfitRate, 2, MidpointRounding.AwayFromZero),
                    },
                };
            }
            #endregion

            return stockModel.GetStockCacheModel();
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
        private async Task CheckUserSignToken(FirestoreGeneralRequest request, UserFirebaseModel userFirebaseModel = null)
        {
            if (userFirebaseModel == null)
            {
                userFirebaseModel = await GetUser(request);
            }
            if (userFirebaseModel == null) throw new Exception(); // TODO: Throw UserNotFound Exception
            if (!userFirebaseModel.LastSignedToken.Equals(request.UserToken)) throw new Exception(); // TODO: Throw TokenUnvalid Exception
        }
        #endregion

        #region User

        public async Task<UserFirebaseModel> GetUser(FirestoreGeneralRequest request)
        {
            FirestoreDb db = FirebaseHelper.Shared.Db;
            var userSnapshot = await db.Collection(FirestoreCollection.Users.Value()).Document(request.UserID).GetSnapshotAsync();
            if (!userSnapshot.Exists) { return null; }
            var userFirebaseModel = userSnapshot.ConvertTo<UserFirebaseModel>();
            userFirebaseModel.UserReference = userSnapshot.Reference;
            return userFirebaseModel;
        }

        public async Task<List<string>> GetFavoriteStockCodes(FirestoreGeneralRequest request)
        {
            var userFirebaseModel = await GetUser(request);
            await CheckUserSignToken(request, userFirebaseModel);
            return userFirebaseModel.FavoriteStocks;
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

        public async Task EditFavorite(EditFavoriteListRequest request)
        {
            var userFirebaseModel = await GetUser(request);
            await CheckUserSignToken(request, userFirebaseModel);
            if (request.Code == null) throw new NullReferenceException();
            if (userFirebaseModel.FavoriteStocks.Contains(request.Code))
            {
                await userFirebaseModel.UserReference.UpdateAsync("favorite_stocks", FieldValue.ArrayRemove(request.Code));
            }
            else
            {
                await userFirebaseModel.UserReference.UpdateAsync("favorite_stocks", FieldValue.ArrayUnion(request.Code));
            }
        }

        #endregion
    }
}
