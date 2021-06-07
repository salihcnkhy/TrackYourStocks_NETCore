using Core.Base;
using Feature.Assets.Model.GetAsset;
using Feature.Assets.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Handler.Assets
{
    public class GetAssetInformationHandler : RequestHandler<AssetsUseCase>, IRequestHandler<GetAssetInformationRequest, GetAssetInformationResponse>
    {
        public Task<GetAssetInformationResponse> Handle(GetAssetInformationRequest request)
        {
            return UseCase.GetAssetInformation(request);
        }
    }
}
