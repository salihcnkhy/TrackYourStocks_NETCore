using Core.Base;
using Feature.UserInformation.Model;
using Feature.UserInformation.Model.Favorite;
using Feature.UserInformation.UseCase;
using System.Threading.Tasks;

namespace Feature.UserInformation.Handler
{
    public class GetFavoriteListHandler : RequestHandler<UserInformationUseCase>, IRequestHandler<GetFavoriteListRequest, GetFavoriteListResponse>
    {
        public Task<GetFavoriteListResponse> Handle(GetFavoriteListRequest request)
        {
            return UseCase.GetFavoriteList(request);
        }
    }
}
