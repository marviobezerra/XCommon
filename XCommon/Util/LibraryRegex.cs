namespace XCommon.Util
{
    public static class LibraryRegex
    {
        public static string Phone
        {
            get
            {
                return @"\(\d{2}\)\d{4}-\d{4}";
            }
        }

        public static string Email
        {
            get
            {
                return @"(^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$)";
            }
        }

        public static string URL
        {
            get
            {
                return @"(:?https?://|www\.|^[\w\d]+)(\S+\b)\.(\S+\b)*";
            }
        }
        
        public static string CPF
        {
            get
            {
                return @"\d{3}\.\d{3}\.\d{3}-\d{2}";
            }
        }

        public static string CNPJ
        {
            get
            {
                return @"\d{2}[.]\d{3}[.]\d{3}/\d{4}-\d{2}";
            }
        }
    }
}
