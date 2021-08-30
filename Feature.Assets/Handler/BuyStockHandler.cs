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
    public class BuyStockHandler : RequestHandler<AssetsUseCase>, IRequestHandler<BuyStockRequest, BuyStockResponse>
    {
        public Task<BuyStockResponse> Handle(BuyStockRequest request)
        {
            return UseCase.BuyStock(request);
        }
    }
}
