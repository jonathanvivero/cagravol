namespace System
{
    public static class BooleanExtensions
    {
        public static string ToSqlString(this bool _me)
        {
            return _me ? "1" : "0";
        }
    }
}
