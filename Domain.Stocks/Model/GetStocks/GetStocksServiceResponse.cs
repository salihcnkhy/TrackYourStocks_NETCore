using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStocksServiceResponse
    {
        public bool Success { get; set; }
        public List<GetStocksServiceValueObject> ValueObjects { get; set; }
        public bool IsContinue { get; set; }
        public GetStocksServiceResponse(int pageSize, QuerySnapshot querySnapshot)
        {
            ValueObjects = new List<GetStocksServiceValueObject>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var stockValueObject = documentSnapshot.ConvertTo<GetStocksServiceValueObject>();
                    if (stockValueObject != null)
                    {
                        ValueObjects.Add(stockValueObject);
                    }
                }
            }
            IsContinue = querySnapshot.Documents.Count == pageSize;
            Success = true;
        }

        public GetStocksServiceResponse()
        {

        }
    }
}
