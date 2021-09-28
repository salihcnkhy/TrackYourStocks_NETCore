using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserInformation.Model
{
    public class GetFavoriteListServiceResponse : Response
    {
        public List<string> FavoriteList { get; set; }
    }
}
