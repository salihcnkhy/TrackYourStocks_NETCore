using Core.Base;
using Domain.Assets.Model;
using Domain.Assets.Service;
using Feature.Assets.Model.GetAsset;
using System.Threading.Tasks;


namespace Feature.Assets.UseCase
{
    public class AssetsUseCase : UseCase<AssetsService>
    {
        public async Task<GetAssetInformationResponse> GetMinimizedAssetInformation(GetAssetInformationRequest request)
        {
            await Api.GetMinimizedAssetInformation(new GetAssetInformationServiceRequest());
            return null;
        }
    }
}
