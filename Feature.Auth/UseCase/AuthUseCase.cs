using Core.Base;
using Core.Extensions;
using Domain.Auth.Model;
using Domain.Auth.Service;
using Feature.Auth.Model;
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
          
                SignUpResponse signUpResponse = new SignUpResponse() 
                { 
                    IsSuccess = apiResponse.UserID.IsNotNullOrEmpty(),
                    UserID = apiResponse.UserID, 
                    UserToken = apiResponse.UserToken 
                };

                return signUpResponse;
            }
            catch(Firebase.Auth.FirebaseAuthException e)
            {
                SignUpResponse signUpResponse = new SignUpResponse()
                {
                    IsSuccess = false,
                    ErrorReason = e.Reason
                };

            return signUpResponse;
            }
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            try
            {
                SignInServiceRequest signInServiceRequest = new SignInServiceRequest() { Email = request.Email, Password = request.Password };
                var apiResponse = await Api.SignIn(signInServiceRequest);
                SignInResponse signInResponse = new SignInResponse() 
                { 
                    IsSuccess = apiResponse.UserID.IsNotNullOrEmpty(), 
                    UserID = apiResponse.UserID,
                    UserToken = apiResponse.UserToken
                };
                return signInResponse;
            }
            catch (Firebase.Auth.FirebaseAuthException e)
            {
                SignInResponse signInResponse = new SignInResponse()
                {
                    IsSuccess = false,
                    ErrorReason = e.Reason
                };

                return signInResponse;
            }
        }

        public async Task<SendResetPasswordEmailResponse> SendResetPasswordEmail(SendResetPasswordEmailRequest request)
        {
            var serviceRequest = new SendResetPasswordEmailServiceRequest
            {
                Email = request.Email 
            };

            await Api.SendResetPasswordEmail(serviceRequest);
            return new SendResetPasswordEmailResponse();
        }
    }
}
