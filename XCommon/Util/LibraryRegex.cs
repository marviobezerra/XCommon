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
                return @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)";
            }
        }

        public static string URL
        {
            get
            {
                return @"(https?|ftp|file)\://[a-zA-Z0-9\.\-]+(/[a-zA-Z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*";
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
