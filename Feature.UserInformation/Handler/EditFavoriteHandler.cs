using Core.Base;
using Feature.UserInformation.Model;
using Feature.UserInformation.Model.Favorite;
using Feature.UserInformation.UseCase;
using System.Threading.Tasks;

namespace Feature.UserInformation.Handler
{
    public class EditFavoriteHandler : RequestHandler<UserInformationUseCase>, IRequestHandler<EditFavoriteRequest, EditFavoriteResponse>
    {
        public Task<EditFavoriteResponse> Handle(EditFavoriteRequest request)
        {
            return UseCase.EditFavorite(request);
        }
    }
}
