using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.UserInformation.Model.Favorite
{
    public class EditFavoriteRequest : AuthRequiredRequest
    {
        public string Code { get; set; }
    }
}
