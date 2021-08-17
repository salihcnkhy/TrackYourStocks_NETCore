using Core.Base;
using Domain.UserInformation.Model;
using Domain.UserInformation.Service;
using Feature.UserInformation.Model;
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


        public async Task<MarketHistoryListResponse> GetMarketHistoryList(MarketHistoryListRequest request)
        {
            var responseList = await Api.GetMarketHistoryList(new MarketHistoryRequest { UserID = request.UserID, UserToken = request.UserToken });

            var marketHistoryResponse = new MarketHistoryListResponse
            {
                IsSuccess = true,
                MarkerHistoryList = responseList.Select(a => new MarketHistoryModel
                {
                    Code = a.Code,
                    Date = a.Date,
                    ProcessType = a.ProcessType,
                    Quantity = a.Quantity,
                    UnitPrice = a.UnitPrice,
                    Id = a.Id,
                    LongName = a.LongName,
                    TriggerPrice = a.TriggerPrice,
                }).ToList()
            };

            return marketHistoryResponse;
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
    }
}

