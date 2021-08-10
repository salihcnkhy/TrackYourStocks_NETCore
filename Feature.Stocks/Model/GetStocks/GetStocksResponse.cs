using Core.Base;
using Domain.Stocks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Model
{
    public class GetStocksResponse : Response
    {
        public bool IsContinue { get; set; }
        public List<GetStocksValueObject> ValueObjects { get; set; }

        public GetStocksResponse(GetStocksServiceResponse serviceResponse)
        {
            ValueObjects = serviceResponse.ValueObjects.Select(element => new GetStocksValueObject(element)).ToList();
            IsContinue = serviceResponse.IsContinue;
            IsSuccess = serviceResponse.Success;
        }
    }
}
