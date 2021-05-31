using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public class UseCase<TService> : IUseCase where TService : ApiService, new()
    {
        protected TService Api = new TService();
    }
}
