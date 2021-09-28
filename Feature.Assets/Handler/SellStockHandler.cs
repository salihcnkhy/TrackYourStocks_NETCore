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
    public class SellStockHandler : RequestHandler<AssetsUseCase>, IRequestHandler<SellStockRequest, SellStockResponse>
    {
        public Task<SellStockResponse> Handle(SellStockRequest request)
        {
            return UseCase.SellStock(request);
        }
    }
}
