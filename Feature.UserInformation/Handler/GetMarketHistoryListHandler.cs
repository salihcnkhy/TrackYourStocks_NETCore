using Core.Base;
using Feature.UserInformation.Model;
using Feature.UserInformation.UseCase;
using System.Threading.Tasks;

namespace Feature.UserInformation.Handler
{
    public class GetMarketHistoryListHandler : RequestHandler<UserInformationUseCase>, IRequestHandler<MarketHistoryListRequest, MarketHistoryListResponse>
    {
        public Task<MarketHistoryListResponse> Handle(MarketHistoryListRequest request)
        {
            return UseCase.GetMarketHistoryList(request);
        }
    }
}
