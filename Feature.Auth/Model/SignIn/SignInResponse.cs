using Core.Base;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Model.SignIn
{
    public class SignInResponse : Response
    {
        public string UserID { get; set; }
        public string UserToken { get; set; }
        public string ErrorReason { get; set; }
    }
}
