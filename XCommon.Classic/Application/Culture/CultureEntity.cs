using System;

namespace XCommon.Application.Culture
{
    public class CultureEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string DateFormat { get; set; }
        public bool Default { get; set; }
    }
}
