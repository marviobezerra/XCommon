namespace XCommon.Util
{
    public static class LibraryRegex
    {
        public static string Phone
        {
            get
            {
                return @"^(\([0-9]{2}\))\s?([9]{1})?([0-9]{4})-([0-9]{4})$";
            }
        }

        public static string Email
        {
            get
            {
                return @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			}
        }

        public static string URL
        {
            get
            {
                return @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*";
            }
        }
    }
}
