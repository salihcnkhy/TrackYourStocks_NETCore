﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Model
{
    public class SignInServiceResponse
    {
        public string UserID { get; set; }
        public string UserToken { get; set; }

    }
}
