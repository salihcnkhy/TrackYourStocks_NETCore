﻿using Core.Base;
using Domain.Auth.Model;
using Domain.Auth.Service;
using Feature.Auth.Model.SignIn;
using Feature.Auth.Model.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.UseCase
{
    public class AuthUseCase : UseCase<AuthService>
    {
        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            try
            {
                SignUpServiceRequest signUpServiceRequest = new SignUpServiceRequest() { Email = request.Email, Password = request.Password };
                var apiResponse = await Api.SignUp(signUpServiceRequest);
                SignUpResponse signUpResponse = new SignUpResponse() { Success = apiResponse.Success, UserID = apiResponse.UserID };
                return signUpResponse;
            }
            catch(Exception e)
            {

                return null;
            }
        }
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            try
            {
                SignInServiceRequest signInServiceRequest = new SignInServiceRequest() { Email = request.Email, Password = request.Password };
                var apiResponse = await Api.SignIn(signInServiceRequest);
                SignInResponse signInResponse = new SignInResponse() { Success = apiResponse.Success, UserID = apiResponse.UserID };
                return signInResponse;
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}
