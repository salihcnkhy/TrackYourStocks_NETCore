using Core.Base;
using Feature.Auth.Model;
using Feature.Auth.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Handler
{
    public class SignUpHandler : RequestHandler<AuthUseCase>, IRequestHandler<SignUpRequest, SignUpResponse>
    {
        public Task<SignUpResponse> Handle(SignUpRequest request)
        {
            return UseCase.SignUp(request);
        }
    }
}
