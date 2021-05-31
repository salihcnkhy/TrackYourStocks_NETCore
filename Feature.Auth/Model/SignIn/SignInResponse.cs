using Core.Base;
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
    }
}
