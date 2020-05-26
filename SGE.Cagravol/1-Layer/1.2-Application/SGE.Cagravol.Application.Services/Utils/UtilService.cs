using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Services.Utils
{
    public class UtilService 
        : IUtilService
    {
        /// <summary>
        /// Una cadena pseudoaleatoria de donde se generara la encriptacion
        /// </summary>
        /// <remarks>Puede ser de cualquier tamaño</remarks>
        private const string iFrasePasswd = "15646^&amp;%$3(),>h1j0l3Gz*-+e7hds";
        /// <summary>
        /// Valor para generar la llave de encriptacion.
        /// </summary>
        /// <remarks>Puede ser de cualquier tamaño</remarks>
        private const string iValor = "656^%%43:;2568'32-0}}{][843";
        /// <summary>
        /// Nombre del Algoritmo.
        /// </summary>
        /// <remarks>Puede ser MD5 o SHA1. SHA1 es un poco mas lento pero es mas seguro</remarks>
        private const string iAlgHash = "SHA1";
        /// <summary>
        /// Numero de Iteraciones.
        /// </summary>
        /// <remarks>1 o 2 iteraciones son suficientes</remarks>
        private const int iNumIteraciones = 2;
        /// <summary>
        /// Vector Inicial
        /// </summary>
        /// <remarks>
        /// Debe ser de 16 caracteres exactamente
        /// </remarks>
        private const string iVectorInicial = "4587hst'3smd(@#&amp;";
        /// <summary>
        /// Tamaño de la Llave
        /// </summary>
        /// <remarks>Puede ser de 128, 192 y 256</remarks>
        private const int iTamLlave = 256;
        private const string allowedPasswordChars = @"ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890$%@.,-_!¡¿?#/\·()";
        public IEnumerable<SelectListItem> YesNoAllList(EnumYesNoAll defaultValue = EnumYesNoAll.All)
        {
            var list = new List<SelectListItem>(){
				new SelectListItem{Selected = (defaultValue== EnumYesNoAll.All), Text = CommonResources.NotSelectedAll, Value = ((int)EnumYesNoAll.All).ToString()},
				new SelectListItem{Selected = (defaultValue== EnumYesNoAll.Yes), Text = CommonResources.Yes, Value = ((int)EnumYesNoAll.Yes).ToString()},
				new SelectListItem{Selected = (defaultValue== EnumYesNoAll.No), Text = CommonResources.No, Value = ((int)EnumYesNoAll.No).ToString()}
			};
            return list;
        }

        public string GetRandomStringId(int places = 3)
        {
            StringBuilder result = new StringBuilder();
            char[] baseCipher = new char[]{'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '+', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '-', 
			'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '*', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '_',
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
            int max = baseCipher.Length;
            Random random = new Random();

            for (var x = 1; x <= places; x++)
            {

                int randomNumber = random.Next(0, max);
                result.Append(baseCipher[randomNumber]);
            }

            return result.ToString();
        }

        public string GetSimulatedId()
        {

            return string.Format("{0}{1}", DateTime.UtcNow.ToString("yyyyMMddhhmmss"), this.GetRandomStringId(3));
        }

        public string GetGUID() 
        {
            Guid g;

            g = Guid.NewGuid();

            return g.ToString();            
        }



        /// <summary>
        /// Encripta con el algoritmo TripleDES
        /// </summary>
        /// <param name="cadena">Cadena a encriptar</param>
        /// <returns>Cadena encriptada</returns>
        public string EncriptarTripleDES(string cadena)
        {
            byte[] resultados;

            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider provHash = new MD5CryptoServiceProvider();
            byte[] llaveTDES = provHash.ComputeHash(utf8.GetBytes(iFrasePasswd));
            TripleDESCryptoServiceProvider algTDES = new TripleDESCryptoServiceProvider()
            {
                Key = llaveTDES,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            // Obtenemos el array de bytes de nuestra cadena a tratar
            byte[] datoEncriptar = utf8.GetBytes(cadena);
            try
            {
                // Generemos en encriptador para nuestro proceso
                ICryptoTransform encriptador = algTDES.CreateEncryptor();
                resultados = encriptador.TransformFinalBlock(datoEncriptar, 0, datoEncriptar.Length);
            }
            finally
            {
                // Liberemos los recursos
                algTDES.Clear();
                provHash.Clear();
            }

            return Convert.ToBase64String(resultados);
        }

        /// <summary>
        /// Descripta con el algoritmo TripleDES
        /// </summary>
        /// <param name="cadena">Cadena a desencriptar</param>
        /// <returns>Cadena desencriptada</returns>
        public string DesencriptarTripleDES(string cadena)
        {
            byte[] resultados;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider provHash = new MD5CryptoServiceProvider();
            byte[] llaveTDES = provHash.ComputeHash(utf8.GetBytes(iFrasePasswd));
            TripleDESCryptoServiceProvider algTDES = new TripleDESCryptoServiceProvider()
            {
                Key = llaveTDES,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] datoADesencriptar = Convert.FromBase64String(cadena);
            try
            {
                ICryptoTransform desencr = algTDES.CreateDecryptor();
                resultados = desencr.TransformFinalBlock(datoADesencriptar, 0, datoADesencriptar.Length);
            }
            finally
            {
                algTDES.Clear();
                provHash.Clear();
            }

            return utf8.GetString(resultados);
        }


        /// <summary>
        /// Descripta con el algoritmo TripleDES, dando una Passfrase diferente
        /// </summary>
        /// <param name="cadena">Cadena a desencriptar</param>
        /// <param name="secret">Cadena a desencriptar</param>
        /// <returns>Cadena desencriptada</returns>
        public string DesencriptarFinalUserPasswordHashWithTripleDES(string cadena)
        {
            byte[] resultados;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider provHash = new MD5CryptoServiceProvider();
            byte[] llaveTDES = provHash.ComputeHash(utf8.GetBytes(iFrasePasswd));
            TripleDESCryptoServiceProvider algTDES = new TripleDESCryptoServiceProvider()
            {
                Key = llaveTDES,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] datoADesencriptar = Convert.FromBase64String(cadena);
            try
            {
                ICryptoTransform desencr = algTDES.CreateDecryptor();
                resultados = desencr.TransformFinalBlock(datoADesencriptar, 0, datoADesencriptar.Length);
            }
            finally
            {
                algTDES.Clear();
                provHash.Clear();
            }

            return utf8.GetString(resultados);
        }

        /// <summary>
        /// Encripta con el algoritmo SHA1
        /// </summary>
        /// <param name="cadena">Cadena a encriptar</param>
        /// <returns>Cadena encriptada</returns>
        public string EncriptarSHA1(string cadena)
        {
            // Generamos los arrays de Bytes de nuestras cadenas. Como iVectorInicial y iValor son cadenas
            // normales solo usamos Encoding.ASCII
            byte[] aVectorInicial = Encoding.ASCII.GetBytes(iVectorInicial);
            byte[] aValorRand = Encoding.ASCII.GetBytes(iValor);
            // Dado que cadena puede contener caracteres UNICODE, usaremos UTF8
            byte[] aCadena = Encoding.UTF8.GetBytes(cadena);

            // Generemos la contraseña
            PasswordDeriveBytes cont = new PasswordDeriveBytes(iFrasePasswd, aValorRand, iAlgHash, iNumIteraciones);
            // Obtengamos el array de la llave. Dividido en Bytes. (8 bits)
            byte[] aLlave = cont.GetBytes(iTamLlave / 8);

            // Usemos la clase Rijndael para la llave simetrica y usemos el modo Cipher Block Chaining (CBC)
            RijndaelManaged llaveSimetrica = new RijndaelManaged() { Mode = CipherMode.CBC };
            // Generemos el encriptador
            ICryptoTransform enc = llaveSimetrica.CreateEncryptor(aLlave, aVectorInicial);

            // Definamos donde tendremos los datos encriptados
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write);

            // Encriptemos
            cs.Write(aCadena, 0, aCadena.Length);

            // Terminemos
            cs.FlushFinalBlock();

            // Bajemos nuestros datos encriptados a un array de bytes
            byte[] aCipher = ms.ToArray();

            // Liberar la memoria de nuestros datos encriptados
            ms.Close();
            cs.Close();

            // Regresmos nuestro dato encriptado como una cadena base64
            return Convert.ToBase64String(aCipher);
        }

        /// <summary>
        /// Descripta con el algoritmo TripleDES
        /// </summary>
        /// <param name="cadena">Cadena a desencriptar</param>
        /// <returns>Cadena desencriptada</returns>
        public string DesencriptarSHA1(string cadena)
        {
            // Generamos los arrays de Bytes de nuestras cadenas. Como iVectorInicial y iValor son cadenas
            // normales solo usamos Encoding.ASCII
            byte[] aVectorInicial = Encoding.ASCII.GetBytes(iVectorInicial);
            byte[] aValorRand = Encoding.ASCII.GetBytes(iValor);
            // Convertimos nuesta cadena encriptada (cipher) a un arrar
            byte[] aCipher = Convert.FromBase64String(cadena);

            // Generemos la contraseña
            PasswordDeriveBytes cont = new PasswordDeriveBytes(iFrasePasswd, aValorRand, iAlgHash, iNumIteraciones);
            // Obtengamos el array de la llave. Dividido en Bytes. (8 bits)
            byte[] aLlave = cont.GetBytes(iTamLlave / 8);

            // Usemos la clase Rijndael para la llave simetrica y usemos el modo Cipher Block Chaining (CBC)
            RijndaelManaged llaveSimetrica = new RijndaelManaged() { Mode = CipherMode.CBC };

            // Generemos el desencriptador
            ICryptoTransform desenc = llaveSimetrica.CreateDecryptor(aLlave, aVectorInicial);

            // Definamos donde tendremos los datos encriptados
            MemoryStream ms = new MemoryStream(aCipher);
            CryptoStream cs = new CryptoStream(ms, desenc, CryptoStreamMode.Read);

            // Definamos el arrar donde se colocaran nuestros datos desencriptados
            byte[] aCadena = new byte[aCipher.Length];

            // Comenzamos a desencriptar
            int tamB = cs.Read(aCadena, 0, aCadena.Length);

            // Liberemos la memoria
            ms.Close();
            cs.Close();

            // regresemos nuestra cadena desecriptada usando UTF8
            return Encoding.UTF8.GetString(aCadena, 0, tamB);

        }

        public IResultModel ReviewPasswordSecurity(string password, string passwordConfirmation)
        {
            IResultModel rv = new ResultModel();

            if (password != passwordConfirmation)
            {
                rv.OnError(ErrorResources.PasswordsDontMatch);
            }
            else if (password.Length < 6)
            { 
                rv.OnError(ErrorResources.PasswordLengthLestThan_Format.sf(6));
            }
            else if (!password.MatchCharacters(allowedPasswordChars))
            {
                rv.OnError(ErrorResources.PaswordAllowOnlyFollowingChars);
            }
            else
            {
                rv.OnSuccess();
            }

            return rv;
        }

        public string GetRandomSessionKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 20)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        public string DecryptPasswordAES(string dataToDecrypt, string encKey, string encIV)
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                string roundtrip;
                //Settings
                myRijndael.Mode = CipherMode.CBC;
                myRijndael.Padding = PaddingMode.PKCS7;
                myRijndael.FeedbackSize = 128;

                byte[] keybytes = Convert.FromBase64String(encKey);
                byte[] iv = Convert.FromBase64String(encIV);


                //DECRYPT FROM CRIPTOJS
                byte[] encrypted = Convert.FromBase64String(dataToDecrypt);

                // Decrypt the bytes to a string.
                try
                {
                    roundtrip = DecryptStringFromBytes(encrypted, keybytes, iv);
                }
                catch (Exception)
                {
                    roundtrip = string.Empty;
                }

                return roundtrip;
            }
        }

        public long ConvertProductVersionToLong(string productVersion, long multiplier = 1000)
        {
            string[] arr = productVersion.Split('.');
            int x;
            long coef = 1;
            long final = 0;
            long q = 0;

            for (x = arr.Length - 1; x >= 0; x--)
            { 
                if (long.TryParse(arr[x], out q))
                {
                    final = final + (q * coef);
                }
                coef = coef * multiplier;
            }

            return final;
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {

                //rijAlg.KeySize = 128 / 8;
                rijAlg.IV = IV;
                rijAlg.Key = Key;


                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

    }
}
