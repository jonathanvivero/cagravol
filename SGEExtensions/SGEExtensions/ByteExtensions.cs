namespace System
{
    public static class ByteExtensions
    {
        public static string ToString(this byte[] _this)
        {
            string res;
            res = System.Text.Encoding.UTF8.GetString(_this);
            return res;
        }
    }
}
