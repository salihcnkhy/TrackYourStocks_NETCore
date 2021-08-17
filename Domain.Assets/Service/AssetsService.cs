using Core.Base;
using Core.Cache;
using Core.Firebase;
using Core.Firebase.Auth.Model;
using Domain.Assets.Model;
using Firebase.Service.Models;
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
            var firebasePortfolioListResponse = await service.GetUserPortfolioList(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            return new GetAssetInformationServiceResponse() { PortfolioFirebaseModelList = firebasePortfolioListResponse };
        }
    }
}
