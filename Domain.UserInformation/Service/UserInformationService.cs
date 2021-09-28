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

        public async Task<GetFavoriteListServiceResponse> GetFavoriteList(GetFavoriteListServiceRequest request)
        {
            var service = new FirebaseService();
            var response = await service.GetFavoriteStockCodes(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            return new GetFavoriteListServiceResponse { FavoriteList = response ?? new List<string>() };
        }

        public async Task EditFavorite(EditFavoriteServiceRequest request)
        {
            var service = new FirebaseService();
            await service.EditFavorite(new EditFavoriteListRequest { Code = request.Code, UserID =  request.UserID, UserToken = request.UserToken });
        }
    }
}
