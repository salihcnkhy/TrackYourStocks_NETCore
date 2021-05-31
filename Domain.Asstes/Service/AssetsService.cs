using Core.Base;
using Core.Firebase;
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
        public async Task<GetAssetInformationServiceResponse> GetMinimizedAssetInformation(GetAssetInformationServiceRequest request)
        {
            var service = new FirebaseService();
            var firebaseResponse = await service.GetMinimizedAssets();
            
            return null;
        }

        public Task<GetAssetInformationServiceResponse> GetAssetInformation(GetAssetInformationServiceRequest request)
        {

            return null;
        }
    }
}
