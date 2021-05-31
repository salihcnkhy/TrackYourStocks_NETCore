using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    [Route("api/[controller]")]
    public abstract class ApiController: ControllerBase
    {
        public async Task<TResponse> SendAsyncRequest<TRequest, TResponse, THandler>(TRequest request) where TRequest : IRequest where TResponse : IResponse where THandler : IRequestHandler<TRequest, TResponse>, new()
        {
            THandler handler = new THandler();
            return await handler.Handle(request);
        }
    }
}
