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
        public async Task<IResponse> SendAsyncRequest<TRequest, TResponse, THandler>(TRequest request) where TRequest : IRequest where TResponse : IResponse where THandler : IRequestHandler<TRequest, TResponse>, new()
        {
            try
            {
                THandler handler = new THandler();
                var response = await handler.Handle(request);
                response.IsSuccess = true;
                return response;
            } 
            catch(ErrorException e)
            {
                return new ErrorResponse { 
                    ExceptionType = e.ExceptionType,
                    Message = e.Message,
                    IsSuccess = false 
                };
            }
        }
    }
}
