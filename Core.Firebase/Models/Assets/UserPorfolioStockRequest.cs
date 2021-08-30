using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{
    public class UserPorfolioStockRequest : FirestoreGeneralRequest
    {
       public string Code { get; set; }
    }
}
