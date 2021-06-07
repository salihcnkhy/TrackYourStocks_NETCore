using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model.GetAsset
{
    public class GetAssetInformationRequest : Request
    {
        public string UserID { get; set; }
    }
}
