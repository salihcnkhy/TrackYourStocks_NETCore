using Core.Base;
using Feature.Auth.Handler;
using Feature.Auth.Model;
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
        public Task<IResponse> SignUp([FromBody] SignUpRequest request)
        {
            return SendAsyncRequest<SignUpRequest, SignUpResponse, SignUpHandler>(request);
        }

        [HttpPost("SignIn")]
        public Task<IResponse> SignIn([FromBody] SignInRequest request)
        {
            return SendAsyncRequest<SignInRequest, SignInResponse, SignInHandler>(request);
        }

        [HttpPost("SendResetPasswordEmail")]
        public Task<IResponse> SignIn([FromBody] SendResetPasswordEmailRequest request)
        {
            return SendAsyncRequest<SendResetPasswordEmailRequest, SendResetPasswordEmailResponse, SendResetPasswordEmailHandler>(request);
        }
    }
}
