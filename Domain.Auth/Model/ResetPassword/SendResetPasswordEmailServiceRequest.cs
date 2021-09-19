using Core.Base;

namespace Domain.Auth.Model
{
    public class SendResetPasswordEmailServiceRequest : Request
    {
        public string Email { get; set; }
    }
}
