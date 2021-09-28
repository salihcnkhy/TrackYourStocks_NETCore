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
    public class SendResetPasswordEmailHandler : RequestHandler<AuthUseCase>, IRequestHandler<SendResetPasswordEmailRequest, SendResetPasswordEmailResponse>
    {
        public Task<SendResetPasswordEmailResponse> Handle(SendResetPasswordEmailRequest request)
        {
            return UseCase.SendResetPasswordEmail(request);
        }
    }
}
