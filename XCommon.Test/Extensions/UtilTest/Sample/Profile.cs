using System;

namespace XCommon.Test.Extensions.UtilTest.Sample
{
    [Flags]
    public enum Profile
    {
        User = 0,
        Supervisor = 1,
        Manager = 2,
        Director = 4,
        CEO = 8,
        SuperUser = 16
    }
}
