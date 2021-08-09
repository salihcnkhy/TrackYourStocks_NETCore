using Core.Base;
using Feature.Auth.Handler;
using Feature.Auth.Model.SignIn;
using Feature.Auth.Model.SignUp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Controller
{
    public class AuthController : ApiController
    {
        [HttpPost("SignUp")]
        public Task<SignUpResponse> SignUp([FromBody] SignUpRequest request)
        {
            return SendAsyncRequest<SignUpRequest, SignUpResponse, SignUpHandler>(request);
        }

        [HttpPost("SignIn")]
        public Task<SignInResponse> SignIn([FromBody] SignInRequest request)
        {
            return SendAsyncRequest<SignInRequest, SignInResponse, SignInHandler>(request);
        }
    }
}
