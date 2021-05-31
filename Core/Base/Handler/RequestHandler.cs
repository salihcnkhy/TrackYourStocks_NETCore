using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
   public abstract class RequestHandler<TUseCase> where TUseCase : IUseCase, new()
    {
        protected TUseCase UseCase = new TUseCase();
    }
}
