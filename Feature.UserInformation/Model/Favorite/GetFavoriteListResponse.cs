using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.UserInformation.Model.Favorite
{
    public class GetFavoriteListResponse : Response
    {
        public List<string> FavoriteList { get; set; }
    }
}
