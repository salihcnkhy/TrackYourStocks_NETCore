using Core.Base;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Model
{
    public class SignUpResponse : Response
    {
        public string UserID { get; set; }
        public string UserToken { get; set; }
        public AuthErrorReason ErrorReason { get; set; }
    }
}
