using Core.Base;
using Feature.Assets.Handler;
using Feature.Assets.Model;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("BuyStock")]
        public Task<BuyStockResponse> BuyStock([FromBody] BuyStockRequest request)
        {
            return SendAsyncRequest<BuyStockRequest, BuyStockResponse, BuyStockHandler>(request);
        }

        [HttpPost("SellStock")]
        public Task<SellStockResponse> SellStock([FromBody] SellStockRequest request)
        {
            return SendAsyncRequest<SellStockRequest, SellStockResponse, SellStockHandler>(request);
        }
    }
}
