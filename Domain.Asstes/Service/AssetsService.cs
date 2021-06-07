using Core.Base;
using Core.Cache;
using Core.Firebase;
using Core.Firebase.Auth.Model;
using Domain.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Service
{
    public class AssetsService: ApiService
    {

        public async Task<GetAssetInformationServiceResponse> GetAssetInformation(GetAssetInformationServiceRequest request)
        {
            var service = new FirebaseService();
            var firebasePortfolioListResponse = await service.GetUserPortfolioList(new UserFirebaseModel { ID = request.UserID });
            return new GetAssetInformationServiceResponse(firebasePortfolioListResponse);
        }
    }
}
