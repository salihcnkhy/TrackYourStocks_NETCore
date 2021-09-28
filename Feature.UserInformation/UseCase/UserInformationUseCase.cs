using Core.Base;
using Domain.UserInformation.Model;
using Domain.UserInformation.Service;
using Feature.UserInformation.Model;
using Feature.UserInformation.Model.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.UserInformation.UseCase
{
    public class UserInformationUseCase : UseCase<UserInformationService>
    {
        public async Task<NotificationListResponse> GetNotificationList(NotificationListRequest request)
        {
            var apiResponse = await Api.GetNotificationList(new NotificationRequest { UserID = request.UserID, UserToken = request.UserToken });
            NotificationListResponse notificationListResponse = new NotificationListResponse
            {
                IsSuccess = true,
                NotificationList = apiResponse.Select(r => new NotificationModel
                {
                    Body = r.Body,
                    Date = r.Date,
                    HasShown = r.HasShown,
                    IconName = r.IconName,
                    Id = r.Id,
                    Title = r.Title,
                }).ToList()
            };

            return notificationListResponse;
        }

        public async Task<AlarmListResponse> GetAlarmList(AlarmListRequest request)
        {
            var responseList = await Api.GetAlarmList(new AlarmRequest { UserID = request.UserID, UserToken = request.UserToken });
            var alarmListResponse = new AlarmListResponse
            {
                IsSuccess = true,
                AlarmList = responseList.Select(a => new AlarmModel
                {
                    Code = a.Code,
                    ConditionType = a.ConditionType,
                    Id = a.Id,
                    LongName = a.LongName,
                    TriggerPrice = a.TriggerPrice,
                }).ToList()
            };

            return alarmListResponse;
        }

        public async Task<GetFavoriteListResponse> GetFavoriteList(GetFavoriteListRequest request)
        {
            var responseList = await Api.GetFavoriteList(new GetFavoriteListServiceRequest { UserID = request.UserID, UserToken = request.UserToken });
            var favoriteListResponse = new GetFavoriteListResponse
            {
                IsSuccess = true,
                FavoriteList = responseList.FavoriteList,
            };

            return favoriteListResponse;
        }

        public async Task<EditFavoriteResponse> EditFavorite(EditFavoriteRequest request)
        {
            await Api.EditFavorite(new EditFavoriteServiceRequest { Code = request.Code, UserID = request.UserID, UserToken = request.UserToken });
            var editFavorite = new EditFavoriteResponse
            {
                IsSuccess = true,
            };

            return editFavorite;
        }
    }
}

