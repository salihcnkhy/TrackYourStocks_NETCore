using Core.Cache;
using Core.Extensions;
using Core.Firebase.Assets.Model;
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
