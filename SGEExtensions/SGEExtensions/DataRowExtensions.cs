using System.Data;

namespace System.Data
{
    public static class DataRowExtensions
    {
        public static int ToInteger(this DataRow _me, string fieldName, int defaultValue = -1)
        {
            int result = defaultValue;
            try
            {
                int? _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<int?>(fieldName) : null;

                if (_fieldValue.HasValue)
                {
                    result = _fieldValue.Value;
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static Double ToDouble(this DataRow _me, string fieldName, Double defaultValue = 0d)
        {
            double result = defaultValue;
            try
            {
                Double? _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<Double?>(fieldName) : null;

                if (_fieldValue.HasValue)
                {
                    result = _fieldValue.Value;
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }


        public static DateTime ToDateTime(this DataRow _me, string fieldName, DateTime defaultValue = default(DateTime))
        {
            DateTime result = defaultValue;
            try
            {
                DateTime? _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<DateTime?>(fieldName) : null;

                if (_fieldValue.HasValue)
                {
                    result = _fieldValue.Value;
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static string ToString(this DataRow _me, string fieldName, string defaultValue = "")
        {
            string result = defaultValue;
            try
            {
                string _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<string>(fieldName) : null;

                if (!string.IsNullOrWhiteSpace(_fieldValue))
                {
                    result = _fieldValue.Trim();
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static bool ToBool(this DataRow _me, string fieldName, bool defaultValue = false)
        {
            bool result = defaultValue;
            try
            {
                bool? _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<bool?>(fieldName) : null;

                if (_fieldValue.HasValue)
                {
                    result = _fieldValue.Value;
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static byte ToByte(this DataRow _me, string fieldName, byte defaultValue = 0)
        {
            byte result = defaultValue;
            try
            {
                byte? _fieldValue = _me.Table.Columns.Contains(fieldName) ? _me.Field<byte?>(fieldName) : null;

                if (_fieldValue.HasValue)
                {
                    result = _fieldValue.Value;
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }
    }
}
