using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest where TResponse : IResponse
    {
         Task<TResponse> Handle(TRequest request);
    }
}
