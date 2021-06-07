using Core.Base;
using Feature.Assets.Handler.Assets;
using Feature.Assets.Model.GetAsset;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Controller
{
    public class AssetController : ApiController
    {
        [HttpPost("GetAssetInformation")]
        public Task<GetAssetInformationResponse> GetAssetInformation([FromBody] GetAssetInformationRequest request)
        {
            return SendAsyncRequest<GetAssetInformationRequest, GetAssetInformationResponse, GetAssetInformationHandler>(request);
        }
    }
}
