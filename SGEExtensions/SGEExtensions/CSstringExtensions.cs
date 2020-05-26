
//namespace System
//{
//    public static class CSstringExtensions
//    {
//        /// <summary>
//        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
//        /// </summary>
//        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
//        /// <param name="arg0"> El objeto al que se va a aplicar formato.</param>
//        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
//        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
//        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
//        /// <remarks></remarks>
//        public static string sf(this string me, object arg0)
//        {
//            return string.Format(me, arg0);
//        }


//        /// <summary>
//        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
//        /// </summary>
//        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
//        /// <param name="arg0">Primer objeto al que se va a dar formato.</param>
//        /// <param name="arg1">Segundo objeto al que se va a dar formato.</param>
//        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
//        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
//        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
//        /// <remarks></remarks>
//        public static string sf(this string me, object arg0, object arg1)
//        {
//            return string.Format(me, arg0, arg1);
//        }


//        /// <summary>
//        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
//        /// </summary>
//        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
//        /// <param name="arg0">Primer objeto al que se va a dar formato.</param>
//        /// <param name="arg1">Segundo objeto al que se va a dar formato.</param>
//        /// <param name="arg2">Tercer objeto al que se va a dar formato.</param>
//        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
//        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
//        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
//        /// <remarks></remarks>
//        public static string sf(this string me, object arg0, object arg1, object arg2)
//        {
//            return string.Format(me, arg0, arg1, arg2);
//        }


//        /// <summary>
//        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
//        /// </summary>
//        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
//        /// <param name="args">Matriz de objetos que contiene cero o más objetos a los que se va a aplicar formato.</param>
//        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
//        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
//        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
//        /// <remarks></remarks>
//        public static string sf(this string me, params object[] args)
//        {
//            return string.Format(me, args);
//        }

//        /// <summary>
//        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
//        /// </summary>
//        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
//        /// <param name="provider">Objeto que proporciona información de formato específica de la referencia cultural.</param>
//        /// <param name="args">Matriz de objetos que contiene cero o más objetos a los que se va a aplicar formato.</param>
//        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
//        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
//        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
//        /// <remarks></remarks>
//        public static string sf(this string me, System.IFormatProvider provider, params object[] args)
//        {
//            return string.Format(provider, me, args);
//        }

//        /// <summary>
//        /// Reverse a string
//        /// </summary>
//        /// <param name="_me"></param>
//        /// <returns></returns>
//        public static string Reverse(this string _me)
//        {
//            char[] charArray = _me.ToCharArray();
//            Array.Reverse(charArray);
//            return new string(charArray);
//        }

//        /// <summary>
//        /// Convierte una cadena en formato numérico con decimales a un formato numérico en el cual el separador decimal es el parámetro "decimalComa"
//        /// </summary>
//        /// <param name="_me"></param>
//        /// <param name="decimalComma">Carácter indicando separador decimal</param>
//        /// <returns></returns>
//        public static string ReverseDecimalFormat(this string _me, string decimalComma = ",")
//        {
//            string thousandPeriod = ".";

//            int i = _me.Reverse().IndexOf(',');

//            if (i == 0 || i == 1 || i == 2)
//            {
//                return _me.Replace(thousandPeriod, "");
//            }


//            if (decimalComma == thousandPeriod)
//            {
//                thousandPeriod = ",";
//            }

//            return _me.Replace(decimalComma, "").Replace(thousandPeriod, decimalComma);
//        }

//        /// <summary>
//        /// Evalua la cadena principal, y en caso que sea null, nothing, o esté vacía, devuelve una alternativa. Si la alternativa es incorrecta, devuelve cadena vacía
//        /// </summary>
//        /// <param name="me"></param>
//        /// <param name="alternativeString"> cadena alternativa válida</param>
//        /// <returns></returns>
//        public static string Oor(this string me, object alternativeString)
//        {
//            string result = null;

//            if ((me != null) && !string.IsNullOrEmpty(me))
//            {
//                result = me;
//            }
//            else
//            {
//                try
//                {
//                    result = (string)alternativeString;
//                }
//                catch (Exception)
//                {
//                    result = string.Empty;
//                }
//            }

//            return result;
//        }


//        public static int ToInteger(this string me, int defaultValue = 0)
//        {

//            int parsed = 0;

//            if (int.TryParse(me, out parsed))
//            {
//                return parsed;
//            }
//            else
//            {
//                return defaultValue;
//            }
//        }

//        public static double ToDouble(this string me, double defaultValue = 0d)
//        {
//            double result;

//            if (!double.TryParse(me.ReverseDecimalFormat(), out result))
//            {
//                result = defaultValue;
//            }

//            return result;
//        }

//        public static string CleanLoginEmail(this string login)
//        {

//            return login;

//            //string result = login;
//            //if (!string.IsNullOrWhiteSpace(login))
//            //{
//            //    result = login.Replace("-", "_");
//            //}

//            //return result;
//        }

//        public static bool MatchCharacters(this string _this, string matchPattern)
//        {
//            for (var x = 0; x < _this.Length; x++)
//            {
//                if (!matchPattern.Contains(_this[0].ToString()))
//                {
//                    return false;
//                }
//            }

//            return true;
//        }

//        public static byte[] ToByteArray(this string _this)
//        {
//            byte[] res;

//            res = System.Text.Encoding.UTF8.GetBytes(_this);

//            return res;
//        }

//        public static long ToLong(this string _this, long defaultValue = 0)
//        {
//            long q;

//            if (long.TryParse(_this, out q))
//            {
//                return q;
//            }
//            else
//            {
//                return defaultValue;
//            }

//        }

//        public static int ToInt(this string _this, int defaultValue = 0)
//        {
//            int q;

//            if (int.TryParse(_this, out q))
//            {
//                return q;
//            }
//            else
//            {
//                return defaultValue;
//            }

//        }

//    }
//}
