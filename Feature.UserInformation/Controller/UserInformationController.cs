using Core.Base;
using Feature.UserInformation.Handler;
using Feature.UserInformation.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.UserInformation.Controller
{
    public class UserInformationController : ApiController
    {
        [HttpPost("GetUserAlarmList")]
        public Task<AlarmListResponse> GetUserAlarmList([FromBody] AlarmListRequest request)
        {
            return SendAsyncRequest<AlarmListRequest, AlarmListResponse, GetAlarmListHandler>(request);
        }

        [HttpPost("GetUserNotificationList")]
        public Task<NotificationListResponse> GetUserNotificationList([FromBody] NotificationListRequest request)
        {
            return SendAsyncRequest<NotificationListRequest, NotificationListResponse, GetNotificationListHandler>(request);
        }

        [HttpPost("GetUserMarkerHistoryList")]
        public Task<MarketHistoryListResponse> GetUserMarkerHistoryList([FromBody] MarketHistoryListRequest request)
        {
            return SendAsyncRequest<MarketHistoryListRequest, MarketHistoryListResponse, GetMarketHistoryListHandler>(request);
        }
    }
}
