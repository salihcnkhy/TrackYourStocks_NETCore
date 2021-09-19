using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Model
{
    public class SendResetPasswordEmailRequest : Request
    {
        public string Email { get; set; }
    }
}
