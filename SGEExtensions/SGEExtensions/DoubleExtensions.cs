namespace System
{
    public static class DoubleExtensions
    {
        public static string ToSqlString(this double _me)
        {
            return _me.ToString().Replace(".", "").Replace(",", ".");
        }
    }
}
