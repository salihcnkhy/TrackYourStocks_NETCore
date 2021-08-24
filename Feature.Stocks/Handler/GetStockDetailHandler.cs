using Core.Base;
using Feature.Stocks.Model;
using Feature.Stocks.UseCase;
using System.Threading.Tasks;

namespace Feature.Stocks.Handler
{
    public class GetStockDetailHandler : RequestHandler<StocksUseCase>, IRequestHandler<GetStockDetailRequest, GetStockDetailResponse>
    {
        public Task<GetStockDetailResponse> Handle(GetStockDetailRequest request)
        {
            return UseCase.GetStockDetail(request);
        }
    }
}
