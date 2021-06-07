using Core.Base;
using Core.Extensions;
using Domain.Assets.Model;
using Domain.Assets.Service;
using Feature.Assets.Model.GetAsset;
using System.Threading.Tasks;


namespace Feature.Assets.UseCase
{
    public class AssetsUseCase : UseCase<AssetsService>
    {
        public async Task<GetAssetInformationResponse> GetAssetInformation(GetAssetInformationRequest request)
        {
            if(request.UserID.IsNullOrEmpty())
            {
                return new GetAssetInformationResponse();
            }

            var serviceResponse = await Api.GetAssetInformation(new GetAssetInformationServiceRequest() { UserID = request.UserID });
            return new GetAssetInformationResponse(serviceResponse);
        }
    }
}
