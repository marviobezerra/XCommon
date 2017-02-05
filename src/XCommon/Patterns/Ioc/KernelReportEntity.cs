namespace XCommon.Patterns.Ioc
{
    public class KernelReportEntity
    {
        public string MapFrom { get; set; }

        public string MapTo { get; set; }

        public int CountNewInstance { get; set; }

        public int CountCacheInstance { get; set; }
    }
}
