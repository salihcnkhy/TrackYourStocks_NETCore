using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public enum ExceptionType
    {
        other = -2, // should have subError code
        undefined = -1,
        AuthInformationsMissing,
        TokenFailed,
        UserNotFound,
        NotEnoughtStocksToSell
    }

    public enum SubErrorType
    {
        defaultCode = -1,
        stockListWasNull,
        stockListWasEmpty,
        stockDayInformationWasNull,
    }

    public static class ExceptionMessages
    {
        public static string GetMessage(this ExceptionType type, SubErrorType subErrorType = SubErrorType.defaultCode)
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
                case ExceptionType.other:
                    return "Şu anda işleminizi gerçekleştiremiyoruz.\n(Hata kodu:" + ((int)subErrorType).ToString() + ")";
            }
            return "Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.";
        }
    }
}
