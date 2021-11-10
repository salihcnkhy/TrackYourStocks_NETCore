using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Extension
{
    public static class AuthErrorExtension
    {
        public static string GetExceptionMessage(this AuthErrorReason value)
        {
            var message = "Şu anda işlemizi gerçekleştiremiyoruz.";
            switch (value)
            {
                case AuthErrorReason.InvalidEmailAddress:
                    message = "Geçersiz E-mail adresi.";
                    break;
                case AuthErrorReason.EmailExists:
                    message = "E-mail kullanılmakta. Şifrenizi hatırlamıyorsanız, \"Şifremi Unuttum\" bölümünden şifre yenileme e-postası alabilirsiniz.";
                    break;
                case AuthErrorReason.TooManyAttemptsTryLater:
                    message = "Çok fazla deneme yapıldı. Lütfen daha sonra tekrar deneyiniz.";
                    break;
                case AuthErrorReason.WeakPassword:
                    message = "Zayıf şifre. Lütfen büyük-küçük harf ve sayı kullanınız.";
                    break;
                case AuthErrorReason.WrongPassword:
                    message = "E-mail veya şifre yanlış. Lütfen tekrar deneyiniz.";
                    break;
                case AuthErrorReason.UnknownEmailAddress:
                    message = "E-mail veya şifre yanlış. Lütfen tekrar deneyiniz.";
                    break;
            }
            return message;
        }
    }
}
