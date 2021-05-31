using Core.Base;
using Core.Firebase;
using Domain.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Service
{
    public class AuthService : ApiService
    {
        public async Task<SignUpServiceResponse> SignUp(SignUpServiceRequest request)
        {
            var service = new FirebaseService();
            var response = await service.SignUp(request.Email, request.Password);
            return new SignUpServiceResponse() { Success = true, UserID = response.User.LocalId };
        }
        public async Task<SignInServiceResponse> SignIn(SignInServiceRequest request)
        {
            var service = new FirebaseService();
            var response = await service.SignIn(request.Email, request.Password);
            return new SignInServiceResponse() { Success = true, UserID = response.User.LocalId };
        }
    }
}
