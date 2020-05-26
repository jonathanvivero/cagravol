using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Services.Utils
{
    public interface IUtilService
    {

        IEnumerable<SelectListItem> YesNoAllList(EnumYesNoAll defaultValue = EnumYesNoAll.All);
        string GetRandomStringId(int places = 3);
        string GetSimulatedId();
        string GetGUID();
        string EncriptarTripleDES(string cadena);
        string DesencriptarTripleDES(string cadena);
        string DesencriptarFinalUserPasswordHashWithTripleDES(string cadena);
        string EncriptarSHA1(string cadena);
        string DesencriptarSHA1(string cadena);
        string GetRandomSessionKey();
        string DecryptPasswordAES(string dataToDecrypt, string encKey, string encIV);
        long ConvertProductVersionToLong(string productVersion, long multiplier = 1000);
        IResultModel ReviewPasswordSecurity(string password, string passwordConfirmation);
    }
}
