using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public enum ExceptionType
    {
        TokenFailed,
        UserNotFound,
    }

    public static class ExceptionMessages
    {
        public static string GetMessage(this ExceptionType type)
        {
            switch (type)
            {
                case ExceptionType.UserNotFound:
                    return "Kullanıcı Bulunamadı.";
                case ExceptionType.TokenFailed:
                    return "Hesabınız başka bir cihazda açık görünüyor. Lütfen tekrar giriş yapınız.";
            }
            return "Bir hata oluştu. Daha sonra tekrar deneyiniz.";
        }
    }
}
