
namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
        /// </summary>
        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
        /// <param name="arg0"> El objeto al que se va a aplicar formato.</param>
        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
        /// <remarks></remarks>
        public static String sf(this String me, object arg0)
        {
            return String.Format(me, arg0);
        }


        /// <summary>
        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
        /// </summary>
        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
        /// <param name="arg0">Primer objeto al que se va a dar formato.</param>
        /// <param name="arg1">Segundo objeto al que se va a dar formato.</param>
        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
        /// <remarks></remarks>
        public static String sf(this String me, object arg0, object arg1)
        {
            return String.Format(me, arg0, arg1);
        }


        /// <summary>
        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
        /// </summary>
        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
        /// <param name="arg0">Primer objeto al que se va a dar formato.</param>
        /// <param name="arg1">Segundo objeto al que se va a dar formato.</param>
        /// <param name="arg2">Tercer objeto al que se va a dar formato.</param>
        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
        /// <remarks></remarks>
        public static String sf(this String me, object arg0, object arg1, object arg2)
        {
            return String.Format(me, arg0, arg1, arg2);
        }


        /// <summary>
        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
        /// </summary>
        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
        /// <param name="args">Matriz de objetos que contiene cero o más objetos a los que se va a aplicar formato.</param>
        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
        /// <remarks></remarks>
        public static String sf(this String me, params object[] args)
        {
            return String.Format(me, args);
        }

        /// <summary>
        /// Alias de String.Format: Reemplaza el elemento de formato de una cadena especificada por la representación de cadena de un objeto correspondiente de una matriz especificada.
        /// </summary>
        /// <param name="_me">Una cadena de formato compuesto (vea Comentarios).</param>
        /// <param name="provider">Objeto que proporciona información de formato específica de la referencia cultural.</param>
        /// <param name="args">Matriz de objetos que contiene cero o más objetos a los que se va a aplicar formato.</param>
        /// <returns>Copia de format en la que los elementos de formato se han reemplazado por la representación de cadena de los objetos correspondientes de args</returns>
        /// <exception cref="System.ArgumentNullException">format o args es null. </exception>
        /// <exception cref="System.FormatException">format no es válido. O bien El índice de un elemento de formato es menor que cero o mayor o igual que la longitud de la matriz args.</exception>
        /// <remarks></remarks>
        public static String sf(this String me, System.IFormatProvider provider, params object[] args)
        {
            return String.Format(provider, me, args);
        }

        /// <summary>
        /// Reverse a string
        /// </summary>
        /// <param name="_me"></param>
        /// <returns></returns>
        public static String Reverse(this String _me)
        {
            char[] charArray = _me.ToCharArray();
            Array.Reverse(charArray);
            return new String(charArray);
        }

        /// <summary>
        /// Convierte una cadena en formato numérico con decimales a un formato numérico en el cual el separador decimal es el parámetro "decimalComa"
        /// </summary>
        /// <param name="_me"></param>
        /// <param name="decimalComma">Carácter indicando separador decimal</param>
        /// <returns></returns>
        public static String ReverseDecimalFormat(this String _me, string decimalComma = ",")
        {
            String thousandPeriod = ".";

            int i = _me.Reverse().IndexOf(',');

            if (i == 0 || i == 1 || i == 2)
            {
                return _me.Replace(thousandPeriod, "");
            }


            if (decimalComma == thousandPeriod)
            {
                thousandPeriod = ",";
            }

            return _me.Replace(decimalComma, "").Replace(thousandPeriod, decimalComma);
        }

        /// <summary>
        /// Evalua la cadena principal, y en caso que sea null, nothing, o esté vacía, devuelve una alternativa. Si la alternativa es incorrecta, devuelve cadena vacía
        /// </summary>
        /// <param name="me"></param>
        /// <param name="alternativeString"> cadena alternativa válida</param>
        /// <returns></returns>
        public static String Oor(this String me, object alternativeString)
        {
            string result = null;

            if ((me != null) && !string.IsNullOrEmpty(me))
            {
                result = me;
            }
            else
            {
                try
                {
                    result = (String)alternativeString;
                }
                catch (Exception)
                {
                    result = string.Empty;
                }
            }

            return result;
        }


        public static int ToInteger(this String me, int defaultValue = 0)
        {

            int parsed = 0;

            if (int.TryParse(me, out parsed))
            {
                return parsed;
            }
            else
            {
                return defaultValue;
            }
        }

        public static double ToDouble(this String me, double defaultValue = 0d)
        {
            double result;

            if (!double.TryParse(me.ReverseDecimalFormat(), out result))
            {
                result = defaultValue;
            }

            return result;
        }


        public static String CleanLoginEmail(this String login)
        {
            string result = login;
            if (!string.IsNullOrWhiteSpace(login))
            {
                result = login.Replace("-", "_");
            }

            return result;
        }

        public static bool MatchCharacters(this String _this, String matchPattern)
        {
            for (var x = 0; x < _this.Length; x++)
            {
                if (!matchPattern.Contains(_this[0].ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public static byte[] ToByteArray(this String _this)
        {
            byte[] res;

            res = System.Text.Encoding.UTF8.GetBytes(_this);

            return res;
        }

        public static long ToLong(this String _this, long defaultValue = 0)
        {
            long q;

            if (long.TryParse(_this, out q))
            {
                return q;
            }
            else
            {
                return defaultValue;
            }

        }

        public static int ToInt(this String _this, int defaultValue = 0)
        {
            int q;

            if (int.TryParse(_this, out q))
            {
                return q;
            }
            else
            {
                return defaultValue;
            }

        }
    }
}
