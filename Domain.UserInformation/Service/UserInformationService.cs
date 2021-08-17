using Core.Base;
using Core.Firebase;
using Domain.UserInformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Service.Models;

namespace Domain.UserInformation.Service
{
    public class UserInformationService : ApiService
    {
        public async Task<List<AlarmServiceModel>> GetAlarmList(AlarmRequest request)
        {
            var service = new FirebaseService();
            var responseList = await service.GetActiveAlarms(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            var response = responseList.Select(a => new AlarmServiceModel
            {
                Code = a.Code,
                ConditionType = a.ConditionType,
                Id = a.Id,
                LongName = a.LongName,
                TriggerPrice = a.TriggerPrice,
            }).ToList();
            return response;
        }

        public async Task<List<NotificationServiceModel>> GetNotificationList(NotificationRequest request)
        {
            var service = new FirebaseService();
            var responseList = await service.GetNotifications(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            var response = responseList.Select(a => new NotificationServiceModel
            {
                Body = a.Body,
                Date = a.Date.ToDateTime(),
                HasShown = a.HasShown,
                IconName = a.IconName,
                Id = a.Id,
                Title = a.Title,
            }).ToList();
            return response;
        }

        public async Task<List<MarketHistoryServiceModel>> GetMarketHistoryList(MarketHistoryRequest request)
        {
            var service = new FirebaseService();
            var responseList = await service.GetMarketHistory(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            var response = responseList.Select(a => new MarketHistoryServiceModel
            {
                Code = a.Code,
                Date = a.Date.ToDateTime(),
                ProcessType = a.ProcessType,
                Quantity = a.Quantity,
                UnitPrice = a.UnitPrice,
                Id = a.Id,
                LongName = a.LongName,
            }).ToList();
            return response;
        }
    }
}
