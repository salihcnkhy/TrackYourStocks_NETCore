using Core.Base;
using Feature.UserInformation.Handler;
using Feature.UserInformation.Model;
using Feature.UserInformation.Model.Favorite;
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
        public Task<IResponse> GetUserAlarmList([FromBody] AlarmListRequest request)
        {
            return SendAsyncRequest<AlarmListRequest, AlarmListResponse, GetAlarmListHandler>(request);
        }

        [HttpPost("GetUserNotificationList")]
        public Task<IResponse> GetUserNotificationList([FromBody] NotificationListRequest request)
        {
            return SendAsyncRequest<NotificationListRequest, NotificationListResponse, GetNotificationListHandler>(request);
        }

        [HttpPost("GetFavoriteList")]
        public Task<IResponse> GetFavoriteList([FromBody] GetFavoriteListRequest request)
        {
            return SendAsyncRequest<GetFavoriteListRequest, GetFavoriteListResponse, GetFavoriteListHandler>(request);
        }

        [HttpPost("EditFavorite")]
        public Task<IResponse> EditFavorite([FromBody] EditFavoriteRequest request)
        {
            return SendAsyncRequest<EditFavoriteRequest, EditFavoriteResponse, EditFavoriteHandler>(request);
        }
    }
}
