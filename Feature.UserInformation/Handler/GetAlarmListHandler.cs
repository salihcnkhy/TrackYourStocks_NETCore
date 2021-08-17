using Core.Base;
using Feature.UserInformation.Model;
using Feature.UserInformation.UseCase;
using System.Threading.Tasks;

namespace Feature.UserInformation.Handler
{
    public class GetAlarmListHandler : RequestHandler<UserInformationUseCase>, IRequestHandler<AlarmListRequest, AlarmListResponse>
    {
        public Task<AlarmListResponse> Handle(AlarmListRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
