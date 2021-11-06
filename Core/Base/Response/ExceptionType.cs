using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public enum ExceptionType
    {
        undefined = -1,
        AuthInformationsMissing,
        TokenFailed,
        UserNotFound,
        NotEnoughtStocksToSell
    }

    public static class ExceptionMessages
    {
        public static string GetMessage(this ExceptionType type)
        {
            switch (type)
            {
                case ExceptionType.AuthInformationsMissing:
                    return "Şu anda işleminizi gerçekleştiremiyoruz. (Code:" + 0.ToString() + ")";
                case ExceptionType.UserNotFound:
                    return "Kullanıcı Bulunamadı.";
                case ExceptionType.TokenFailed:
                    return "Hesabınız başka bir cihazda açık görünüyor. Lütfen tekrar giriş yapınız.";
                case ExceptionType.NotEnoughtStocksToSell:
                    return "Sahip olduğunuz hisse miktarından fazla miktarda satış işlemi yapamazsınız.";

            }
            return "Bir hata oluştu. Daha sonra tekrar deneyiniz.";
        }
    }
}
