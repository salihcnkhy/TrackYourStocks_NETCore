using Core.Cache;
using Core.Extensions;
using Firebase.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Model
{
    public class GetAssetInformationServiceResponse
    {
        public List<PortfolioFirebaseModel> PortfolioFirebaseModelList { get; set; }
    }
}
