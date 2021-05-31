using Core.Base;
using Feature.Auth.Model.SignIn;
using Feature.Auth.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Handler
{
    public class SignInHandler : RequestHandler<AuthUseCase>, IRequestHandler<SignInRequest, SignInResponse>
    {
        public Task<SignInResponse> Handle(SignInRequest request)
        {
            return UseCase.SignIn(request);
        }
    }
}
