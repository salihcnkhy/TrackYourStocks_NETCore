using Core.Base;
using Feature.UserInformation.Model;
using Feature.UserInformation.UseCase;
using System.Threading.Tasks;

namespace Feature.UserInformation.Handler
{
    public class GetNotificationListHandler : RequestHandler<UserInformationUseCase>, IRequestHandler<NotificationListRequest, NotificationListResponse>
    {
        public Task<NotificationListResponse> Handle(NotificationListRequest request)
        {
            return UseCase.GetNotificationList(request);
        }
    }
}
