using Core.Base;
using Feature.Assets.Model;
using Feature.Assets.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Handler
{
    public class GetAssetInformationHandler : RequestHandler<AssetsUseCase>, IRequestHandler<GetAssetInformationRequest, GetAssetInformationResponse>
    {
        public Task<GetAssetInformationResponse> Handle(GetAssetInformationRequest request)
        {
            return UseCase.GetAssetInformation(request);
        }
    }
}
