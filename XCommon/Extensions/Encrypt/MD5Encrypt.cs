namespace XCommon.Extensions.Encrypt
{
    public static class MD5Encrypt
    {
        public static string ToMD5(this string value, bool upperCase = true)
        {
            return XCommon.Util.Functions.GetMD5(value, upperCase);
        }
    }
}
